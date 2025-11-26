using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Repository.IRepository;
using System.Reflection;

namespace PitangBoosterVendas.Repository.Imp.Repositories
{
    public class ReflectionTestRepository(Context context) : IReflectionTestRepository
    {
        /// <summary>
        /// Exemplo 1: Type - Obter informações sobre um tipo
        /// </summary>
        public Dictionary<string, object> ExemploType(Type tipo)
        {
            var info = new Dictionary<string, object>
            {
                ["Nome"] = tipo.Name,
                ["NomeCompleto"] = tipo.FullName ?? "N/A",
                ["Namespace"] = tipo.Namespace ?? "N/A",
                ["Assembly"] = tipo.Assembly.GetName().Name ?? "N/A",
                ["EhAbstrato"] = tipo.IsAbstract,
                ["EhSelado"] = tipo.IsSealed,
                ["EhClasse"] = tipo.IsClass,
                ["EhInterface"] = tipo.IsInterface,
                ["ClasseBase"] = tipo.BaseType?.Name ?? "nenhuma",
                ["Interfaces"] = tipo.GetInterfaces().Select(i => i.Name).ToList(),
                ["Propriedades"] = tipo.GetProperties().Select(p => $"{p.PropertyType.Name} {p.Name}").ToList()
            };

            return info;
        }

        /// <summary>
        /// Exemplo 2: PropertyInfo - Ler e escrever propriedades dinamicamente
        /// </summary>
        public Dictionary<string, object?> ExemploPropertyInfo(object instancia)
        {
            var tipo = instancia.GetType();
            var propriedades = tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var resultado = new Dictionary<string, object?>();

            foreach (var prop in propriedades)
            {
                if (prop.CanRead)
                {
                    var valor = prop.GetValue(instancia);
                    resultado[prop.Name] = new
                    {
                        Tipo = prop.PropertyType.Name,
                        Valor = valor,
                        PodeEscrever = prop.CanWrite,
                        PodeLer = prop.CanRead
                    };
                }
            }

            return resultado;
        }

        /// <summary>
        /// Exemplo 3: PropertyInfo - Modificar propriedades dinamicamente
        /// </summary>
        public void ExemploModificarPropriedade(object instancia, string nomePropriedade, object? novoValor)
        {
            var tipo = instancia.GetType();
            var propriedade = tipo.GetProperty(nomePropriedade);

            if (propriedade != null && propriedade.CanWrite)
            {
                propriedade.SetValue(instancia, novoValor);
            }
            else
            {
                throw new InvalidOperationException($"Propriedade '{nomePropriedade}' não encontrada ou não pode ser modificada");
            }
        }

        /// <summary>
        /// Exemplo 4: MethodInfo - Invocar métodos dinamicamente
        /// </summary>
        public object? ExemploMethodInfo(object instancia, string nomeMetodo, params object[] parametros)
        {
            var tipo = instancia.GetType();
            var metodo = tipo.GetMethod(nomeMetodo, BindingFlags.Public | BindingFlags.Instance);

            if (metodo == null)
            {
                throw new MethodAccessException($"Método '{nomeMetodo}' não encontrado");
            }

            var resultado = metodo.Invoke(instancia, parametros);

            return new
            {
                NomeMetodo = metodo.Name,
                TipoRetorno = metodo.ReturnType.Name,
                Parametros = metodo.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}").ToList(),
                Resultado = resultado
            };
        }

        /// <summary>
        /// Exemplo 5: FieldInfo - Acessar campos (incluindo privados)
        /// </summary>
        public Dictionary<string, object?> ExemploFieldInfo(Type tipo, bool incluirPrivados = false)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;

            if (incluirPrivados)
            {
                bindingFlags |= BindingFlags.NonPublic;
            }

            var campos = tipo.GetFields(bindingFlags);
            var resultado = new Dictionary<string, object?>();

            foreach (var campo in campos)
            {
                resultado[campo.Name] = new
                {
                    TipoCampo = campo.FieldType.Name,
                    EhPublico = campo.IsPublic,
                    EhPrivado = campo.IsPrivate,
                    EhReadonly = campo.IsInitOnly,
                    EhStatico = campo.IsStatic
                };
            }

            return resultado;
        }

        /// <summary>
        /// Exemplo 6: ConstructorInfo - Analisar construtores
        /// </summary>
        public List<string> ExemploConstructorInfo(Type tipo)
        {
            var construtores = tipo.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            var resultado = new List<string>();

            foreach (var construtor in construtores)
            {
                var parametros = construtor.GetParameters();
                var parametrosStr = string.Join(", ", parametros.Select(p => $"{p.ParameterType.Name} {p.Name}"));
                resultado.Add($"Constructor({parametrosStr})");
            }

            return resultado;
        }

        /// <summary>
        /// Exemplo 7: ConstructorInfo - Criar instância usando construtor específico
        /// </summary>
        public object? ExemploCriarComConstructor(Type tipo)
        {
            var construtor = tipo.GetConstructor(Type.EmptyTypes);

            if (construtor != null)
            {
                return construtor.Invoke(null);
            }

            throw new InvalidOperationException($"Construtor sem parâmetros não encontrado para {tipo.Name}");
        }

        /// <summary>
        /// Exemplo 8: Assembly - Listar tipos do assembly
        /// </summary>
        public Dictionary<string, object> ExemploAssembly(string nomeAssembly)
        {
            var assembly = Assembly.Load(nomeAssembly);
            var tipos = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract).ToList();

            var resultado = new Dictionary<string, object>
            {
                ["NomeAssembly"] = assembly.GetName().Name ?? "N/A",
                ["Versao"] = assembly.GetName().Version?.ToString() ?? "N/A",
                ["TotalTipos"] = tipos.Count,
                ["Classes"] = tipos.Where(t => t.IsClass && !t.IsInterface).Select(t => t.Name).Take(10).ToList(),
                ["Interfaces"] = assembly.GetTypes().Where(t => t.IsInterface).Select(t => t.Name).Take(10).ToList()
            };

            return resultado;
        }

        /// <summary>
        /// Exemplo 9: Assembly - Buscar tipos específicos
        /// </summary>
        public List<Type> ExemploBuscarTiposPorNome(string nomeAssembly, string filtro)
        {
            var assembly = Assembly.Load(nomeAssembly);
            return assembly.GetTypes()
                .Where(t => t.Name.Contains(filtro, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// Exemplo 10: Activator - Criar instância a partir do tipo
        /// </summary>
        public object? ExemploActivator(Type tipo)
        {
            if (tipo.IsAbstract || tipo.IsInterface)
            {
                throw new InvalidOperationException($"Não é possível instanciar tipo abstrato ou interface: {tipo.Name}");
            }

            return Activator.CreateInstance(tipo);
        }

        /// <summary>
        /// Exemplo 11: Activator - Criar instância a partir do nome do tipo
        /// </summary>
        public object? ExemploActivatorPorNome(string nomeCompletoTipo)
        {
            var tipo = Type.GetType(nomeCompletoTipo);

            if (tipo == null)
            {
                // Buscar em todos os assemblies carregados
                tipo = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.FullName == nomeCompletoTipo || t.Name == nomeCompletoTipo);
            }

            if (tipo == null)
            {
                throw new TypeLoadException($"Tipo '{nomeCompletoTipo}' não encontrado");
            }

            return Activator.CreateInstance(tipo);
        }

        /// <summary>
        /// Exemplo 12: Comparar dois objetos usando Reflection
        /// </summary>
        public Dictionary<string, (object? ValorAntigo, object? ValorNovo)> ExemploCompararObjetos<T>(T objeto1, T objeto2)
        {
            var diferencas = new Dictionary<string, (object?, object?)>();
            var tipo = typeof(T);
            var propriedades = tipo.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in propriedades)
            {
                if (!prop.CanRead) continue;

                var valor1 = prop.GetValue(objeto1);
                var valor2 = prop.GetValue(objeto2);

                if (!Equals(valor1, valor2))
                {
                    diferencas.Add(prop.Name, (valor1, valor2));
                }
            }

            return diferencas;
        }
    }
}