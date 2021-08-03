namespace SGE.Infra.Filters
{
    public class PontoFiltro : GenericFilter
    {
        public long? UsuarioId { get; set; }

        public bool? Hoje { get; set; }
    }
}
