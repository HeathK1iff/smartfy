using System.Configuration;
using Microsoft.Extensions.Logging;
using Smartfy.Core.Services;
using System.Text.RegularExpressions;
using System.Reflection;
using Smartfy.Core.Exceptions;

namespace Smartfy.Runner
{
    internal class LibraryLoader
    {
        private const string ACCEPTED_CLASS_NAME = "LibraryLoader";
        private const string ACCEPTED_CLASS_METHOD = "CreateService";
        private readonly Configuration _configuration;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceCollection _services;
        private readonly string _path;
        private ILogger<LibraryLoader> _logger;

        public LibraryLoader(string path, Configuration configuration, ILoggerFactory loggerFactory, IServiceCollection services)
        {
            _logger = loggerFactory.CreateLogger<LibraryLoader>();
            _configuration = configuration;
            _loggerFactory = loggerFactory;
            _services = services;
            _path = path;
        }

        public void LoadAll()
        {
            if (!ScanAndGetMethods(_path, out var list))
            {
                _logger.LogInformation("Libraries were not found");
                return;
            }

            var libraries = new Queue<(string Name, MethodInfo Method)>(list);

            var couters = new Dictionary<MethodInfo, int>();

            while (libraries.Count > 0)
            {
                var method = libraries.Dequeue();
                try
                {
                    method.Method.Invoke(null, new object[] { _configuration, _loggerFactory, _services });
                    _logger.LogInformation($"Service {method.Name} is started");
                }
                catch (ServiceNotFoundException ex)
                {
                    int counter;

                    if (couters.TryGetValue(method.Method, out counter))
                    {
                        counter = counter + 1;
                    }

                    couters[method.Method] = counter;

                    if (counter < libraries.Count)
                    {
                        libraries.Enqueue(method);
                    }
                    else
                    {
                        _logger.LogWarning($"Service {method.Name} is not loaded. Error: {ex.Message}");
                    }
                }
                catch (Exception ex) 
                {
                    _logger.LogWarning($"Service {method.Name} is not loaded. Error: {ex.InnerException?.Message ?? ex.Message}");
                }
            }
        }

        private bool ScanAndGetMethods(string path, out List<(string Name, MethodInfo Method)> list)
        {
            list = new List<(string Name, MethodInfo Method)>();

            foreach (var fileName in Directory.GetFiles(path))
            {
                if (!Regex.IsMatch(Path.GetFileName(fileName.ToUpper()), "SMART.+\\.DLL$"))
                    continue;

                System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(fileName);
                Type? classType = GetAcceptedType(assembly);

                if (classType == null)
                {
                    continue;
                }

                MethodInfo? method = GetAcceptedMethod(classType);

                if (method == null)
                {
                    _logger.LogWarning($"Accepted method ({ACCEPTED_CLASS_METHOD}) is not found in LibraryLoader class");
                    continue;
                }

                list.Add((Name: Path.GetFileNameWithoutExtension(fileName), Method: method));
            }

            return list.Count > 0;
        }
        private Type? GetAcceptedType(System.Reflection.Assembly assembly)
        {
            return assembly.GetTypes().FirstOrDefault(f => f.IsClass
                            && f.IsPublic && f.IsSealed
                            && Regex.IsMatch(f.FullName ?? string.Empty, @".+\." + ACCEPTED_CLASS_NAME + "$"));
        }

        private MethodInfo? GetAcceptedMethod(Type classType)
        {
            var method = classType.GetMethod(ACCEPTED_CLASS_METHOD);

            if ((method != null) && (method.IsStatic))
            {
                if (IsAcceptedParameters(method.GetParameters()))
                    return method;
            }

            return null;
        }

        private bool IsAcceptedParameters(ParameterInfo[] parameters)
        {
            if (parameters.Length == 3)
            {
                if ((parameters[0].ParameterType == typeof(System.Configuration.Configuration)) &&
                    (parameters[1].ParameterType == typeof(ILoggerFactory)) &&
                    (parameters[2].ParameterType == typeof(IServiceCollection)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}