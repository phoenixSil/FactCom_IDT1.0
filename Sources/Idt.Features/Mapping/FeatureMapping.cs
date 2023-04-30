using AutoMapper;
using Idt.Domain;
using Idt.Features.Dtos.Adresses;
using Idt.Features.Dtos.Utilisateurs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Idt.Features.Mapping
{
    public class FeatureMapping: Profile
    {
        public FeatureMapping()
        {
            CreateMap<Adresse, AdresseDto>().ReverseMap();
            CreateMap<Adresse, AdresseDetailDto>().ReverseMap();
            CreateMap<Adresse, AdresseACreerDto>().ReverseMap();
            CreateMap<Adresse, AdresseAModifierDto>().ReverseMap();

            CreateMap<Utilisateur, UtilisateurDto>().ReverseMap();
            CreateMap<Utilisateur, UtilisateurDetailDto>().ReverseMap();
            CreateMap<Utilisateur, UtilisateurACreerDto>().ReverseMap();
            CreateMap<Utilisateur, UtilisateurAModifierDto>().ReverseMap();
            CreateMap<Utilisateur, LoggingUserDto>().ReverseMap();
            CreateMap<Utilisateur, LoggedUserDto>().ReverseMap();
        }
    }
}
