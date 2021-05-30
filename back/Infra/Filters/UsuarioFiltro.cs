namespace SGE.Infra.Filters
{
    public class UsuarioFiltro : GenericFilter
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string UserName { get; set; }
        public long? InquilinoId { get; set; }
    }
}
