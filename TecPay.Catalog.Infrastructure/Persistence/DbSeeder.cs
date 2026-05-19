using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecPay.Catalog.Domain.Entities;

namespace TecPay.Catalog.Infrastructure.Persistence
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(CatalogDbContext db)
        {
            await db.Database.EnsureCreatedAsync();

            if (await db.Set_Categoria.AnyAsync())
                return;

            var categoriaComputacion = new Categoria("Computación");
            var categoriaPapeleria = new Categoria("Papelería");
            var categoriaElectronica = new Categoria("Electrónica");
            var categoriaTelefonia = new Categoria("Telefonía");
            var categoriaAccesoriosCelulares = new Categoria("Accesorios para Celulares");
            var categoriaImpresoras = new Categoria("Impresoras");
            var categoriaConsumibles = new Categoria("Consumibles de Impresión");
            var categoriaRedes = new Categoria("Redes y Comunicaciones");
            var categoriaServidores = new Categoria("Servidores");
            var categoriaSoftware = new Categoria("Software");
            var categoriaLicencias = new Categoria("Licencias");
            var categoriaSeguridad = new Categoria("Seguridad Informática");
            var categoriaVideoVigilancia = new Categoria("Videovigilancia");
            var categoriaAudio = new Categoria("Audio");
            var categoriaVideo = new Categoria("Video");
            var categoriaFotografia = new Categoria("Fotografía");
            var categoriaGaming = new Categoria("Gaming");
            var categoriaConsolas = new Categoria("Consolas de Videojuegos");
            var categoriaPerifericos = new Categoria("Periféricos");
            var categoriaAlmacenamiento = new Categoria("Almacenamiento");
            var categoriaMemoriasUSB = new Categoria("Memorias USB");
            var categoriaDiscosDuros = new Categoria("Discos Duros");
            var categoriaSSD = new Categoria("Unidades SSD");
            var categoriaMonitores = new Categoria("Monitores");
            var categoriaTeclados = new Categoria("Teclados");
            var categoriaMouse = new Categoria("Mouse");
            var categoriaWebcams = new Categoria("Webcams");
            var categoriaMicrofonos = new Categoria("Micrófonos");
            var categoriaBocinas = new Categoria("Bocinas");
            var categoriaAudifonos = new Categoria("Audífonos");
            var categoriaProyectores = new Categoria("Proyectores");
            var categoriaSmartHome = new Categoria("Hogar Inteligente");
            var categoriaDomotica = new Categoria("Domótica");
            var categoriaIluminacion = new Categoria("Iluminación");
            var categoriaHerramientas = new Categoria("Herramientas");
            var categoriaFerreteria = new Categoria("Ferretería");
            var categoriaMobiliario = new Categoria("Mobiliario de Oficina");
            var categoriaOficina = new Categoria("Artículos de Oficina");
            var categoriaEscolar = new Categoria("Material Escolar");
            var categoriaArte = new Categoria("Arte y Dibujo");
            var categoriaLibros = new Categoria("Libros");
            var categoriaRevistas = new Categoria("Revistas");
            var categoriaJuguetes = new Categoria("Juguetes");
            var categoriaBebes = new Categoria("Bebés");
            var categoriaSalud = new Categoria("Salud y Cuidado Personal");
            var categoriaBelleza = new Categoria("Belleza");
            var categoriaRopa = new Categoria("Ropa");
            var categoriaCalzado = new Categoria("Calzado");
            var categoriaDeportes = new Categoria("Deportes");
            var categoriaCamping = new Categoria("Camping y Outdoor");
            var categoriaAutomotriz = new Categoria("Automotriz");
            var categoriaMotocicletas = new Categoria("Motocicletas");
            var categoriaRefacciones = new Categoria("Refacciones");
            var categoriaHerramientasElectricas = new Categoria("Herramientas Eléctricas");
            var categoriaJardineria = new Categoria("Jardinería");
            var categoriaMascotas = new Categoria("Mascotas");
            var categoriaAlimentos = new Categoria("Alimentos");
            var categoriaBebidas = new Categoria("Bebidas");
            var categoriaLimpieza = new Categoria("Limpieza");
            var categoriaHogar = new Categoria("Hogar");
            var categoriaCocina = new Categoria("Cocina");
            var categoriaElectrodomesticos = new Categoria("Electrodomésticos");
            var categoriaRefrigeracion = new Categoria("Refrigeración");
            var categoriaAiresAcondicionados = new Categoria("Aires Acondicionados");
            var categoriaConstruccion = new Categoria("Construcción");
            var categoriaMaquinaria = new Categoria("Maquinaria");
            var categoriaIndustrial = new Categoria("Industrial");
            var categoriaSeguridadIndustrial = new Categoria("Seguridad Industrial");
            var categoriaPetróleoGas = new Categoria("Petróleo y Gas");
            var categoriaInstrumentacion = new Categoria("Instrumentación");
            var categoriaValvulas = new Categoria("Válvulas");
            var categoriaBombas = new Categoria("Bombas");
            var categoriaTubosConexiones = new Categoria("Tubos y Conexiones");
            var categoriaLaboratorio = new Categoria("Laboratorio");
            var categoriaEducacion = new Categoria("Educación");
            var categoriaCursos = new Categoria("Cursos y Capacitación");
            var categoriaServicios = new Categoria("Servicios");
            var categoriaConsultoria = new Categoria("Consultoría");
            var categoriaInteligenciaArtificial = new Categoria("Inteligencia Artificial");
            var categoriaRobotica = new Categoria("Robótica");
            var categoriaDrones = new Categoria("Drones");
            var categoriaImpresion3D = new Categoria("Impresión 3D");

            db.Set_Categoria.AddRange(
                categoriaComputacion,
                categoriaPapeleria,
                categoriaElectronica,
                categoriaTelefonia,
                categoriaAccesoriosCelulares,
                categoriaImpresoras,
                categoriaConsumibles,
                categoriaRedes,
                categoriaServidores,
                categoriaSoftware,
                categoriaLicencias,
                categoriaSeguridad,
                categoriaVideoVigilancia,
                categoriaAudio,
                categoriaVideo,
                categoriaFotografia,
                categoriaGaming,
                categoriaConsolas,
                categoriaPerifericos,
                categoriaAlmacenamiento,
                categoriaMemoriasUSB,
                categoriaDiscosDuros,
                categoriaSSD,
                categoriaMonitores,
                categoriaTeclados,
                categoriaMouse,
                categoriaWebcams,
                categoriaMicrofonos,
                categoriaBocinas,
                categoriaAudifonos,
                categoriaProyectores,
                categoriaSmartHome,
                categoriaDomotica,
                categoriaIluminacion,
                categoriaHerramientas,
                categoriaFerreteria,
                categoriaMobiliario,
                categoriaOficina,
                categoriaEscolar,
                categoriaArte,
                categoriaLibros,
                categoriaRevistas,
                categoriaJuguetes,
                categoriaBebes,
                categoriaSalud,
                categoriaBelleza,
                categoriaRopa,
                categoriaCalzado,
                categoriaDeportes,
                categoriaCamping,
                categoriaAutomotriz,
                categoriaMotocicletas,
                categoriaRefacciones,
                categoriaHerramientasElectricas,
                categoriaJardineria,
                categoriaMascotas,
                categoriaAlimentos,
                categoriaBebidas,
                categoriaLimpieza,
                categoriaHogar,
                categoriaCocina,
                categoriaElectrodomesticos,
                categoriaRefrigeracion,
                categoriaAiresAcondicionados,
                categoriaConstruccion,
                categoriaMaquinaria,
                categoriaIndustrial,
                categoriaSeguridadIndustrial,
                categoriaPetróleoGas,
                categoriaInstrumentacion,
                categoriaValvulas,
                categoriaBombas,
                categoriaTubosConexiones,
                categoriaLaboratorio,
                categoriaEducacion,
                categoriaCursos,
                categoriaServicios,
                categoriaConsultoria,
                categoriaInteligenciaArtificial,
                categoriaRobotica,
                categoriaDrones,
                categoriaImpresion3D
            );

            await db.SaveChangesAsync();

            db.Set_Producto.AddRange(
                new Producto("Laptop Business Pro", "TEC-LAP-001", categoriaComputacion.Id, 18500, 8, "Equipo para oficina y desarrollo."),
                new Producto("Monitor 27 pulgadas", "TEC-MON-027", categoriaComputacion.Id, 5200, 15, "Monitor QHD para productividad."),
                new Producto("Teclado Mecánico", "TEC-KEY-100", categoriaComputacion.Id, 1350, 25, "Teclado retroiluminado.")
            );

            await db.SaveChangesAsync();
        }
    }
}
