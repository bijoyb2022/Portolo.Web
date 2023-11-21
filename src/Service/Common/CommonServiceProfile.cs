using AutoMapper;
using Portolo.Common.Data;
using Portolo.Common.Response;
using Portolo.Framework.Utils;

namespace Portolo.Common
{
    public class CommonServiceProfile : Profile
    {
        public CommonServiceProfile()
        {
            this.CreateMap<LanguageTranslator, ResourceDTO>(MemberList.Source)
                .ForSourceMember(s => s.TranslatorId, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.LabelName, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.en_us, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.es_cr, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.it_it, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.zh_cn, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.zh_tw, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.fr_fr, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.nl_nl, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.pt_pt, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.de_de, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.es_es, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.pt_br, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.es_mx, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.pl_pl, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.fi_fi, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.ja_jp, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.ko_kr, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.CreatedBy, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.CreatedOn, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.ModifiedBy, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.ModifiedOn, opt => opt.DoNotValidate())
                .ForAllMembers(c => c.IgnoreIfSourceIsNull());

            this.CreateMap<Language, LanguageDTO>(MemberList.Source)
                .ForMember(dest => dest.LanguageDescription, opt => opt.MapFrom(src => src.LanguageDesc))
                .ForSourceMember(s => s.LanguageID, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.CreatedBy, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.CreatedOn, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.ModifiedBy, opt => opt.DoNotValidate())
                .ForSourceMember(s => s.ModifiedOn, opt => opt.DoNotValidate())
                .ForAllMembers(c => c.IgnoreIfSourceIsNull());

        }
    }
}
