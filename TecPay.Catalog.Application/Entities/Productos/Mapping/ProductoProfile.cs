using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Application.Entities.Productos.DTOs;
using TecPay.Catalog.Domain.Entities;

namespace TecPay.Catalog.Application.Entities.Productos.Mapping
{
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<ProductoCreacionDTO, Producto>();
            CreateMap<Producto, ProductoComboDTO>();
            CreateMap<Producto, ProductoListaDTO>()
                .ForMember(dest => dest.CategoriaNombre, opt => opt.MapFrom(src => src.Categoria.CategoriaNombre));

        }
    }
}
