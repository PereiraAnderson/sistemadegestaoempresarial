using SGE.Infra.Filters;
using SGE.Infra.Utils;
using Xunit;

namespace SGE.Test.OthersTest
{
    public class OthersTest
    {
        [Fact]
        public void PaginacaoTest()
        {
            var paginacao = new Paginacao();
            Assert.False(paginacao.ListaTodos);
            Assert.Equal(1, paginacao.Pagina);
            Assert.Equal(10, paginacao.Tamanho);
            Assert.Equal(0, paginacao.Skip());
        }

        [Fact]
        public void OrdenacaoTest()
        {
            var ordenacao = new Ordenacao();
            Assert.True(ordenacao.OrdenacaoAsc);
            Assert.Equal("Id", ordenacao.OrdenaPor);
        }

        [Fact]
        public void GenericFiltroTest()
        {
            var usuarioFiltro = new UsuarioFiltro();
            Assert.True(usuarioFiltro.Ativo);
            Assert.Null(usuarioFiltro.Includes);
        }

        [Fact]
        public void UsuarioFiltroTest()
        {
            var usuarioFiltro = new UsuarioFiltro();
            Assert.Null(usuarioFiltro.CPF);
            Assert.Null(usuarioFiltro.Login);
            Assert.Null(usuarioFiltro.Nome);
        }

        [Fact]
        public void PontoFiltroTest()
        {
            var pontoFiltro = new PontoFiltro();
        }
    }
}