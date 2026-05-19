using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Entities.Categorias.DTOs;
using TecPay.Catalog.Domain.Entities;

namespace TecPay.Catalog.Application.Entities.Categorias.Mapping
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<CategoriaCreacionDTO, Categoria>();
            CreateMap<Categoria, CategoriaComboDTO>();
            CreateMap<Categoria, CategoriaListaDTO>();
        }
    }
}
