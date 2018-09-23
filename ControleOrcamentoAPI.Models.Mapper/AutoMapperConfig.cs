namespace ControleOrcamentoAPI.Models.Mapper
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Banco, Banco>()
                .ForMember(m => m.ID, o => o.Ignore())
                .ForMember(m => m.UsuarioInclusao, o => o.Ignore())
                .ForMember(m => m.DataInclusao, o => o.Ignore())
                .ForMember(m => m.UsuarioAlteracao, o => o.Ignore())
                .ForMember(m => m.DataAlteracao, o => o.Ignore())
                .ForMember(m => m.UsuarioCancelamento, o => o.Ignore())
                .ForMember(m => m.DataCancelamento, o => o.Ignore());

                cfg.CreateMap<Agencia, Agencia>()
                .ForMember(m => m.ID, o => o.Ignore())
                .ForMember(m => m.UsuarioInclusao, o => o.Ignore())
                .ForMember(m => m.DataInclusao, o => o.Ignore())
                .ForMember(m => m.UsuarioAlteracao, o => o.Ignore())
                .ForMember(m => m.DataAlteracao, o => o.Ignore())
                .ForMember(m => m.UsuarioCancelamento, o => o.Ignore())
                .ForMember(m => m.DataCancelamento, o => o.Ignore());
            });
        }
    }
}
