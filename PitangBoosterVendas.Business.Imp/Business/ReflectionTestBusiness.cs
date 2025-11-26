using log4net;
using PitangBoosterVendas.Business.IBusiness;
using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Repository.IRepository;
using System.Reflection;

namespace PitangBoosterVendas.Business.Imp.Business
{
    public class ReflectionTestBusiness(IReflectionTestRepository _repository) : IReflectionTestBusiness
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ReflectionTestBusiness));

        public async Task ExecutarTestesReflection()
        {
            _log.Info("╔════════════════════════════════════════════════════════════════════╗");
            _log.Info("║          INICIANDO EXEMPLOS DE REFLECTION NO .NET                  ║");
            _log.Info("╚════════════════════════════════════════════════════════════════════╝");

            await Task.Run(() =>
            {
                TesteType();
                TestePropertyInfo();
                TesteMethodInfo();
                TesteFieldInfo();
                TesteConstructorInfo();
                TesteAssembly();
                TesteActivator();
                TesteComparacaoObjetos();
            });

            _log.Info("\n╔════════════════════════════════════════════════════════════════════╗");
            _log.Info("║          EXEMPLOS DE REFLECTION CONCLUÍDOS COM SUCESSO             ║");
            _log.Info("╚════════════════════════════════════════════════════════════════════╝\n");
        }

        private void TesteType()
        {
            _log.Info("\n┌────────────────────────────────────────────────────────────────────┐");
            _log.Info("│ EXEMPLO 1: Type - Obter informações sobre tipos                   │");
            _log.Info("│ Objetivo: Inspecionar metadados de tipos em runtime               │");
            _log.Info("└────────────────────────────────────────────────────────────────────┘");

            try
            {
                // Analisar classe Produto
                var tipoProduto = typeof(Produto);
                var infoProduto = _repository.ExemploType(tipoProduto);

                _log.Info($"\n📦 Informações do tipo '{infoProduto["Nome"]}':");
                _log.Info($"   ├─ Nome Completo: {infoProduto["NomeCompleto"]}");
                _log.Info($"   ├─ Namespace: {infoProduto["Namespace"]}");
                _log.Info($"   ├─ Assembly: {infoProduto["Assembly"]}");
                _log.Info($"   ├─ É Abstrato: {infoProduto["EhAbstrato"]}");
                _log.Info($"   ├─ É Classe: {infoProduto["EhClasse"]}");
                _log.Info($"   └─ Classe Base: {infoProduto["ClasseBase"]}");

                var interfaces = infoProduto["Interfaces"] as List<string>;
                if (interfaces?.Any() == true)
                {
                    _log.Info($"\n   🔌 Interfaces ({interfaces.Count}):");
                    foreach (var iface in interfaces)
                        _log.Info($"      • {iface}");
                }

                var propriedades = infoProduto["Propriedades"] as List<string>;
                if (propriedades?.Any() == true)
                {
                    _log.Info($"\n   📋 Propriedades ({propriedades.Count}):");
                    foreach (var prop in propriedades)
                        _log.Info($"      • {prop}");
                }

                // Analisar classe abstrata Pagamento
                _log.Info("\n📦 Analisando tipo abstrato 'Pagamento':");
                var tipoPagamento = typeof(Pagamento);
                var infoPagamento = _repository.ExemploType(tipoPagamento);
                _log.Info($"   └─ É Abstrato: {infoPagamento["EhAbstrato"]} (não pode ser instanciado diretamente)");

                _log.Info("\n✅ Teste Type concluído!");
                _log.Info("💡 Uso prático: Validação de tipos, documentação automática, ORM");
            }
            catch (Exception ex)
            {
                _log.Error($"❌ Erro no teste Type: {ex.Message}", ex);
            }
        }

        private void TestePropertyInfo()
        {
            _log.Info("\n┌────────────────────────────────────────────────────────────────────┐");
            _log.Info("│ EXEMPLO 2: PropertyInfo - Ler e modificar propriedades            │");
            _log.Info("│ Objetivo: Manipular propriedades sem conhecê-las em compile-time  │");
            _log.Info("└────────────────────────────────────────────────────────────────────┘");

            try
            {
                var produto = new Produto
                {
                    Id = 1,
                    Nome = "Notebook",
                    Preco = 3500.00m,
                    QuantidadeEstoque = 10
                };

                _log.Info("\n📦 Lendo propriedades do objeto Produto:");
                var propriedades = _repository.ExemploPropertyInfo(produto);

                foreach (var prop in propriedades)
                {
                    if (prop.Value != null)
                    {
                        var tipo = prop.Value.GetType();
                        var tipoProp = tipo.GetProperty("Tipo")?.GetValue(prop.Value);
                        var valor = tipo.GetProperty("Valor")?.GetValue(prop.Value);
                        var podeEscrever = tipo.GetProperty("PodeEscrever")?.GetValue(prop.Value);

                        _log.Info($"   • {prop.Key}:");
                        _log.Info($"      ├─ Tipo: {tipoProp}");
                        _log.Info($"      ├─ Valor: {valor}");
                        _log.Info($"      └─ Pode Escrever: {podeEscrever}");
                    }
                }

                _log.Info("\n🔧 Modificando propriedade 'Nome' dinamicamente:");
                _log.Info($"   ├─ Valor Anterior: {produto.Nome}");
                _repository.ExemploModificarPropriedade(produto, "Nome", "Notebook Gaming");
                _log.Info($"   └─ Valor Novo: {produto.Nome}");

                _log.Info("\n🔧 Modificando propriedade 'Preco' dinamicamente:");
                _log.Info($"   ├─ Valor Anterior: R$ {produto.Preco:F2}");
                _repository.ExemploModificarPropriedade(produto, "Preco", 4200.00m);
                _log.Info($"   └─ Valor Novo: R$ {produto.Preco:F2}");

                _log.Info("\n✅ Teste PropertyInfo concluído!");
                _log.Info("💡 Uso prático: Mapeamento objeto-relacional, serialização, binding");
            }
            catch (Exception ex)
            {
                _log.Error($"❌ Erro no teste PropertyInfo: {ex.Message}", ex);
            }
        }

        private void TesteMethodInfo()
        {
            _log.Info("\n┌────────────────────────────────────────────────────────────────────┐");
            _log.Info("│ EXEMPLO 3: MethodInfo - Invocar métodos dinamicamente             │");
            _log.Info("│ Objetivo: Executar métodos descobertos em runtime                 │");
            _log.Info("└────────────────────────────────────────────────────────────────────┘");

            try
            {
                var produto = new Produto
                {
                    Id = 1,
                    Nome = "Mouse Gamer",
                    Preco = 150.00m
                };

                _log.Info("\n🎯 Invocando método 'ToString' dinamicamente:");
                var resultadoToString = _repository.ExemploMethodInfo(produto, "ToString");
                if (resultadoToString != null)
                {
                    var tipo = resultadoToString.GetType();
                    var nomeMetodo = tipo.GetProperty("NomeMetodo")?.GetValue(resultadoToString);
                    var tipoRetorno = tipo.GetProperty("TipoRetorno")?.GetValue(resultadoToString);
                    var resultado = tipo.GetProperty("Resultado")?.GetValue(resultadoToString);

                    _log.Info($"   ├─ Método: {nomeMetodo}");
                    _log.Info($"   ├─ Tipo Retorno: {tipoRetorno}");
                    _log.Info($"   └─ Resultado: {resultado}");
                }

                _log.Info("\n🎯 Invocando método 'GetType' dinamicamente:");
                var resultadoGetType = _repository.ExemploMethodInfo(produto, "GetType");
                if (resultadoGetType != null)
                {
                    var tipo = resultadoGetType.GetType();
                    var nomeMetodo = tipo.GetProperty("NomeMetodo")?.GetValue(resultadoGetType);
                    var resultado = tipo.GetProperty("Resultado")?.GetValue(resultadoGetType);

                    _log.Info($"   ├─ Método: {nomeMetodo}");
                    _log.Info($"   └─ Resultado: {resultado}");
                }

                _log.Info("\n✅ Teste MethodInfo concluído!");
                _log.Info("💡 Uso prático: Frameworks de teste, plugins, injeção de dependência");
            }
            catch (Exception ex)
            {
                _log.Error($"❌ Erro no teste MethodInfo: {ex.Message}", ex);
            }
        }

        private void TesteFieldInfo()
        {
            _log.Info("\n┌────────────────────────────────────────────────────────────────────┐");
            _log.Info("│ EXEMPLO 4: FieldInfo - Acessar campos (incluindo privados)        │");
            _log.Info("│ Objetivo: Inspecionar campos de uma classe                        │");
            _log.Info("└────────────────────────────────────────────────────────────────────┘");

            try
            {
                var tipoProduto = typeof(Produto);

                _log.Info("\n🔍 Analisando campos públicos da classe Produto:");
                var campos = _repository.ExemploFieldInfo(tipoProduto, incluirPrivados: false);

                if (campos.Any())
                {
                    foreach (var campo in campos)
                    {
                        var info = campo.Value as dynamic;
                        _log.Info($"   • {campo.Key}:");
                        _log.Info($"      ├─ Tipo: {info?.TipoCampo}");
                        _log.Info($"      ├─ É Público: {info?.EhPublico}");
                        _log.Info($"      └─ É Readonly: {info?.EhReadonly}");
                    }
                }
                else
                {
                    _log.Info("   └─ Nenhum campo público encontrado (a classe usa apenas propriedades)");
                }

                _log.Info("\n🔍 Analisando todos os campos (incluindo privados):");
                var todosOsCampos = _repository.ExemploFieldInfo(tipoProduto, incluirPrivados: true);
                _log.Info($"   └─ Total de campos: {todosOsCampos.Count}");

                _log.Info("\n✅ Teste FieldInfo concluído!");
                _log.Info("💡 Uso prático: Debugging, testes unitários, serialização profunda");
            }
            catch (Exception ex)
            {
                _log.Error($"❌ Erro no teste FieldInfo: {ex.Message}", ex);
            }
        }

        private void TesteConstructorInfo()
        {
            _log.Info("\n┌────────────────────────────────────────────────────────────────────┐");
            _log.Info("│ EXEMPLO 5: ConstructorInfo - Analisar e usar construtores         │");
            _log.Info("│ Objetivo: Criar instâncias usando construtores específicos        │");
            _log.Info("└────────────────────────────────────────────────────────────────────┘");

            try
            {
                var tipoProduto = typeof(Produto);

                _log.Info("\n🏗️  Analisando construtores da classe Produto:");
                var construtores = _repository.ExemploConstructorInfo(tipoProduto);

                foreach (var construtor in construtores)
                {
                    _log.Info($"   • {construtor}");
                }

                _log.Info("\n🏗️  Criando instância usando ConstructorInfo:");
                var produto = _repository.ExemploCriarComConstructor(tipoProduto);

                if (produto != null)
                {
                    _log.Info($"   ✓ Instância criada com sucesso!");
                    _log.Info($"   ├─ Tipo: {produto.GetType().Name}");
                    _log.Info($"   └─ Hash: {produto.GetHashCode()}");
                }

                _log.Info("\n✅ Teste ConstructorInfo concluído!");
                _log.Info("💡 Uso prático: Factory pattern, IoC containers, deserialização");
            }
            catch (Exception ex)
            {
                _log.Error($"❌ Erro no teste ConstructorInfo: {ex.Message}", ex);
            }
        }

        private void TesteAssembly()
        {
            _log.Info("\n┌────────────────────────────────────────────────────────────────────┐");
            _log.Info("│ EXEMPLO 6: Assembly - Explorar assemblies                         │");
            _log.Info("│ Objetivo: Listar tipos e informações de assemblies carregados     │");
            _log.Info("└────────────────────────────────────────────────────────────────────┘");

            try
            {
                var nomeAssembly = "PitangBoosterVendas.Entity";

                _log.Info($"\n📦 Analisando assembly '{nomeAssembly}':");
                var infoAssembly = _repository.ExemploAssembly(nomeAssembly);

                _log.Info($"   ├─ Nome: {infoAssembly["NomeAssembly"]}");
                _log.Info($"   ├─ Versão: {infoAssembly["Versao"]}");
                _log.Info($"   └─ Total de Tipos: {infoAssembly["TotalTipos"]}");

                var classes = infoAssembly["Classes"] as List<string>;
                if (classes?.Any() == true)
                {
                    _log.Info($"\n   📋 Primeiras 10 classes:");
                    foreach (var classe in classes)
                        _log.Info($"      • {classe}");
                }

                var interfaces = infoAssembly["Interfaces"] as List<string>;
                if (interfaces?.Any() == true)
                {
                    _log.Info($"\n   🔌 Primeiras 10 interfaces:");
                    foreach (var iface in interfaces)
                        _log.Info($"      • {iface}");
                }

                _log.Info("\n🔍 Buscando tipos que contém 'Pagamento':");
                var tiposPagamento = _repository.ExemploBuscarTiposPorNome(nomeAssembly, "Pagamento");
                _log.Info($"   └─ {tiposPagamento.Count} tipos encontrados:");
                foreach (var tipo in tiposPagamento.Take(5))
                    _log.Info($"      • {tipo.Name}");

                _log.Info("\n✅ Teste Assembly concluído!");
                _log.Info("💡 Uso prático: Plugin systems, auto-discovery, análise de dependências");
            }
            catch (Exception ex)
            {
                _log.Error($"❌ Erro no teste Assembly: {ex.Message}", ex);
            }
        }

        private void TesteActivator()
        {
            _log.Info("\n┌────────────────────────────────────────────────────────────────────┐");
            _log.Info("│ EXEMPLO 7: Activator - Criar instâncias dinamicamente             │");
            _log.Info("│ Objetivo: Instanciar objetos a partir de Type ou string           │");
            _log.Info("└────────────────────────────────────────────────────────────────────┘");

            try
            {
                _log.Info("\n🎨 Criando instância usando Type:");
                var tipoProduto = typeof(Produto);
                var produto1 = _repository.ExemploActivator(tipoProduto);

                if (produto1 != null)
                {
                    _log.Info($"   ✓ Instância criada: {produto1.GetType().Name}");
                    _log.Info($"   └─ Hash: {produto1.GetHashCode()}");
                }

                _log.Info("\n🎨 Criando instância usando nome do tipo:");
                var produto2 = _repository.ExemploActivatorPorNome("Produto");

                if (produto2 != null)
                {
                    _log.Info($"   ✓ Instância criada: {produto2.GetType().Name}");
                    _log.Info($"   └─ Namespace: {produto2.GetType().Namespace}");
                }

                _log.Info("\n🎨 Testando com nome completo do tipo:");
                var produto3 = _repository.ExemploActivatorPorNome("PitangBoosterVendas.Entity.Entities.Produto");

                if (produto3 != null)
                {
                    _log.Info($"   ✓ Instância criada com nome completo!");
                }

                _log.Info("\n⚠️  Testando tipo abstrato (deve falhar):");
                try
                {
                    var pagamento = _repository.ExemploActivator(typeof(Pagamento));
                }
                catch (Exception ex)
                {
                    _log.Info($"   └─ Esperado: {ex.Message}");
                }

                _log.Info("\n✅ Teste Activator concluído!");
                _log.Info("💡 Uso prático: Factory pattern, configuração por arquivo, carregamento dinâmico");
            }
            catch (Exception ex)
            {
                _log.Error($"❌ Erro no teste Activator: {ex.Message}", ex);
            }
        }

        private void TesteComparacaoObjetos()
        {
            _log.Info("\n┌────────────────────────────────────────────────────────────────────┐");
            _log.Info("│ EXEMPLO 8: Comparação - Comparar objetos usando Reflection        │");
            _log.Info("│ Objetivo: Detectar mudanças entre duas instâncias                  │");
            _log.Info("└────────────────────────────────────────────────────────────────────┘");

            try
            {
                var produto1 = new Produto
                {
                    Id = 1,
                    Nome = "Mouse",
                    Preco = 50.00m,
                    QuantidadeEstoque = 100
                };

                var produto2 = new Produto
                {
                    Id = 1,
                    Nome = "Mouse RGB",
                    Preco = 80.00m,
                    QuantidadeEstoque = 100
                };

                _log.Info("\n🔍 Comparando dois objetos Produto:");
                _log.Info($"   Produto 1: {produto1.Nome} - R$ {produto1.Preco:F2}");
                _log.Info($"   Produto 2: {produto2.Nome} - R$ {produto2.Preco:F2}");

                var diferencas = _repository.ExemploCompararObjetos(produto1, produto2);

                if (diferencas.Any())
                {
                    _log.Info($"\n   📊 {diferencas.Count} diferença(s) encontrada(s):");
                    foreach (var dif in diferencas)
                    {
                        _log.Info($"      • {dif.Key}:");
                        _log.Info($"         ├─ Antes: {dif.Value.ValorAntigo}");
                        _log.Info($"         └─ Depois: {dif.Value.ValorNovo}");
                    }
                }
                else
                {
                    _log.Info("   └─ Objetos são idênticos");
                }

                _log.Info("\n✅ Teste Comparação concluído!");
                _log.Info("💡 Uso prático: Auditoria, change tracking, sincronização de dados");
            }
            catch (Exception ex)
            {
                _log.Error($"❌ Erro no teste Comparação: {ex.Message}", ex);
            }
        }
    }
}