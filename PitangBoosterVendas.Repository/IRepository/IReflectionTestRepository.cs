using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Entity.Entities;
using PitangBoosterVendas.Repository.IRepository.Base;
using System.Reflection;

namespace PitangBoosterVendas.Repository.IRepository
{
    public interface IReflectionTestRepository
    {
        Dictionary<string, object> ExemploType(Type tipo);
        Dictionary<string, object?> ExemploPropertyInfo(object instancia);
        void ExemploModificarPropriedade(object instancia, string nomePropriedade, object? novoValor);
        object? ExemploMethodInfo(object instancia, string nomeMetodo, params object[] parametros);
        Dictionary<string, object?> ExemploFieldInfo(Type tipo, bool incluirPrivados = false);
        List<string> ExemploConstructorInfo(Type tipo);
        object? ExemploCriarComConstructor(Type tipo);
        Dictionary<string, object> ExemploAssembly(string nomeAssembly);
        List<Type> ExemploBuscarTiposPorNome(string nomeAssembly, string filtro);
        object? ExemploActivator(Type tipo);
        object? ExemploActivatorPorNome(string nomeCompletoTipo);
        Dictionary<string, (object? ValorAntigo, object? ValorNovo)> ExemploCompararObjetos<T>(T objeto1, T objeto2);

    }
}
