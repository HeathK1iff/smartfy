using System.Configuration;
using Microsoft.Extensions.Logging;
using Smartfy.Core.Services;
using System.Text.RegularExpressions;
using System.Reflection;
using Smartfy.Core.Exceptions;

namespace Smartfy.Runner
{
    public class ExternalLibraryLoader
    {
        private readonly string _className;
        private readonly string _methodName;
        private ILogger<ExternalLibraryLoader> _logger;

        public ExternalLibraryLoader(ILoggerFactory loggerFactory, string className, string methodName)
        {
            _logger = loggerFactory.CreateLogger<ExternalLibraryLoader>();
            _className = className;
            _methodName = methodName;
        }

        public void LoadAndInitAll(string path, params object[] parameters)
        {
            if (!ScanAndGetMethods(path, out var list))
            {
                _logger.LogInformation("Libraries were not found");
                return;
            }

            var libraries = new Queue<(string Name, MethodInfo Method)>(list);

            var counters = new Dictionary<MethodInfo, int>();

            while (libraries.Count > 0)
            {
                var method = libraries.Dequeue();
                try
                {
                    method.Method.Invoke(null, parameters);
                    _logger.LogInformation($"Library {method.Name} is started");
                }
                catch (ServiceNotFoundException ex)
                {
                    int counter;

                    if (counters.TryGetValue(method.Method, out counter))
                    {
                        counter = counter + 1;
                    }

                    counters[method.Method] = counter;

                    if (counter < libraries.Count)
                    {
                        libraries.Enqueue(method);
                    }
                    else
                    {
                        _logger.LogWarning($"Library {method.Name} is not loaded. Error: {ex.Message}");
                    }
                }
                catch (Exception ex) 
                {
                    _logger.LogWarning($"Library {method.Name} is not loaded. Error: {ex.InnerException?.Message ?? ex.Message}");
                }
            }
        }

        private bool ScanAndGetMethods(string path, out List<(string Name, MethodInfo Method)> list)
        {
            list = new List<(string Name, MethodInfo Method)>();

            foreach (string fileName in Directory.GetFiles(path, "*.dll"))
            {
                if (!Regex.IsMatch(Path.GetFileName(fileName.ToUpper()), ".+\\.DLL$"))
                    continue;

                System.Reflection.Assembly assembly = null;
                try
                {
                    assembly = System.Reflection.Assembly.LoadFrom(fileName);
                }catch (Exception ex)
                {
                    _logger.LogWarning(ex.Message);
                }

                if (assembly == null)
                {
                    continue;
                }

                Type? classType = GetAcceptedType(assembly);

                if (classType == null)
                {
                    continue;
                }

                MethodInfo? method = GetAcceptedMethod(classType);

                if (method == null)
                {
                    _logger.LogWarning($"Accepted method ({_methodName}) is not found in {_className} class");
                    continue;
                }

                list.Add((Name: Path.GetFileNameWithoutExtension(fileName), Method: method));
            }

            return list.Count > 0;
        }
        private Type? GetAcceptedType(System.Reflection.Assembly assembly)
        {
            return assembly.GetTypes().FirstOrDefault(f => f.IsClass
                            && f.IsPublic && f.IsAbstract
                            && Regex.IsMatch(f.FullName ?? string.Empty, @".+\." + _className + "$"));
        }
        private MethodInfo? GetAcceptedMethod(Type classType)
        {
            var method = classType.GetMethod(_methodName);

            if ((method != null) && (method.IsStatic))
            {
                return method;
            }

            return null;
        }
    }
}