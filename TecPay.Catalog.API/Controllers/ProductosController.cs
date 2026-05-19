using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TecPay.Catalog.Application.Common;
using TecPay.Catalog.Application.Entities.Categorias.Queries;
using TecPay.Catalog.Application.Entities.Productos.Commands;
using TecPay.Catalog.Application.Entities.Productos.DTOs;
using TecPay.Catalog.Application.Entities.Productos.Queries;

namespace TecPay.Catalog.API.Controllers
{
    [ApiController, ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Producto")]
    public sealed class ProductosController(IMediator mediator) : ControllerBase
    {
        [HttpGet(nameof(GetListaProductoFiltroPaginado))]
        public async Task<ActionResult<List<ProductoListaDTO>>> GetListaProductoFiltroPaginado([FromQuery] FiltroFechaParametroDTO filtroFechaParametroDTO, CancellationToken cancellationToken = default)
        {
            var resultado = await mediator.Send
                 (
                     new GetProductoPaginadoQuery(filtroFechaParametroDTO.FiltroFechaParametroParametro, filtroFechaParametroDTO.FiltroFechaParametroParametro, filtroFechaParametroDTO.Pagina, filtroFechaParametroDTO.CantidadRegistrosPorPagina), cancellationToken
                 );

            HttpContext.Response.Headers.Add("cantidadTotalRegistros", resultado.TotalRegistros.ToString());

            return Ok(resultado.Elementos);
        }

        [HttpGet(nameof(GetTodo))]
        public async Task<ActionResult<List<ProductoListaDTO>>> GetTodo(CancellationToken cancellationToken)
        {
            var producto = await mediator.Send
                (
                    new GetProductoAllQuery(), cancellationToken
                );

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpGet(nameof(GetListaProductoComboReload))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<ProductoComboDTO>>> GetListaProductoComboReload(CancellationToken cancellationToken)
        {
            var producto = await mediator.Send
                (
                    new GetProductoComboReloadQuery(), cancellationToken
                );

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpGet(nameof(GetListaProductoPaginado))]
        public async Task<ActionResult<List<ProductoListaDTO>>> GetListaProductoPaginado([FromQuery] PaginacionDTO paginacionDTO, CancellationToken cancellationToken)
        {
            var resultado = await mediator.Send
            (
                new GetProductoPaginadoQuery("", "", paginacionDTO.Pagina, paginacionDTO.CantidadRegistrosPorPagina), cancellationToken
            );

            HttpContext.Response.Headers.Add("cantidadTotalRegistros", resultado.TotalRegistros.ToString());

            return Ok(resultado.Elementos);

        }

        [HttpGet(nameof(GetListaProductoFiltro))]
        public async Task<ActionResult<List<ProductoListaDTO>>> GetListaProductoFiltro([FromQuery] FiltroFechaParametroDTO filtroFechaParametroDTO, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send
                (
                    new GetProductoQuery(filtroFechaParametroDTO.FiltroFechaParametroParametro), cancellationToken)
                );
        }

        [HttpGet(nameof(GetProductoPorId) + "/{id:guid}", Name = nameof(GetProductoPorId))]
        public async Task<ActionResult<ProductoDTO>> GetProductoPorId(Guid id, CancellationToken cancellationToken)
        {
            var producto = await mediator.Send
                (
                    new GetProductoByIdQuery(id), cancellationToken
                );

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }


        [HttpPost(nameof(CreatePorForm))]
        [Authorize(Policy = "CatalogWrite")]
        public async Task<IActionResult> CreatePorForm([FromForm] ProductoCreacionDTO productoCreacionDTO, CancellationToken cancellationToken)
        {
            var created = await mediator.Send
                (
                    new CreateProductoCommand(productoCreacionDTO), cancellationToken
                );

            return CreatedAtAction(nameof(GetProductoPorId), new { id = created.Id, version = "1.0" }, created);
        }


        [HttpPut(nameof(UpdatePorForm) + "/{id:Guid}")]
        [Authorize(Policy = "CatalogWrite")]
        public async Task<IActionResult> UpdatePorForm(Guid id, [FromForm] ProductoCreacionDTO productoCreacionDTO, CancellationToken cancellationToken)
        {
            var producto = await mediator.Send
                (
                    new UpdateProductoCommand(id, productoCreacionDTO), cancellationToken
                );

            if (producto == null)
                return NotFound();

            return Ok(producto);

        }


        [HttpPost(nameof(ExistsCreate))]
        public async Task<bool> ExistsCreate([FromForm] ProductoCreacionDTO productoCreacionDTO, CancellationToken cancellationToken)
        {
            var exists = await mediator.Send
               (
                   new ExistsCreateProductoCommand(productoCreacionDTO), cancellationToken
               );

            return exists;
        }


        [HttpPut(nameof(ExistsUpdate) + "/{id:Guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<bool> ExistsUpdate(Guid id, [FromForm] ProductoCreacionDTO productoCreacionDTO, CancellationToken cancellationToken)
        {
            var exists = await mediator.Send
               (
                   new ExistsUpdateProductoCommand(id, productoCreacionDTO), cancellationToken
               );

            return exists;

        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "CatalogWrite")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            return await mediator.Send
                (
                    new DeleteProductoCommand(id), cancellationToken
                ) ? NoContent() : NotFound();
        }

        [HttpDelete(nameof(DeleteLogico) + "/{id:Guid}")]
        public async Task<ActionResult> DeleteLogico(Guid id, CancellationToken cancellationToken)
        {
            return await mediator.Send
                (
                    new DeleteProductoCommand(id), cancellationToken
                ) ? NoContent() : NotFound();
        }
    }
}
