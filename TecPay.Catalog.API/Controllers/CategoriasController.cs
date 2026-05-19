using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using TecPay.Catalog.API.Helpers;
using TecPay.Catalog.Application.Common;
using TecPay.Catalog.Application.Entities.Categorias.Commands;
using TecPay.Catalog.Application.Entities.Categorias.DTOs;
using TecPay.Catalog.Application.Entities.Categorias.Queries;
using TecPay.Catalog.Domain.Entities;

namespace TecPay.Catalog.API.Controllers
{
    [ApiController, ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Categoria")]
    public sealed class CategoriasController(IMediator mediator) : ControllerBase
    {

        [HttpGet(nameof(GetListaCategoriaFiltroPaginado))]
        public async Task<ActionResult<List<CategoriaListaDTO>>> GetListaCategoriaFiltroPaginado([FromQuery] FiltroFechaParametroDTO filtroFechaParametroDTO, CancellationToken cancellationToken = default)
        {
            
            var resultado = await mediator.Send
                (
                    new GetCategoriaPaginadoQuery(filtroFechaParametroDTO.FiltroFechaParametroParametro, filtroFechaParametroDTO.Pagina, filtroFechaParametroDTO.CantidadRegistrosPorPagina), cancellationToken
                );

            HttpContext.Response.Headers.Add("cantidadTotalRegistros", resultado.TotalRegistros.ToString());

            return Ok(resultado.Elementos);
        }

        [HttpGet(nameof(GetTodo))]
        public async Task<ActionResult<List<CategoriaListaDTO>>> GetTodo(CancellationToken cancellationToken)
        {
            var categoria = await mediator.Send
                (
                    new GetCategoriaAllQuery(), cancellationToken
                );

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpGet(nameof(GetListaCategoriaComboReload))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<CategoriaComboDTO>>> GetListaCategoriaComboReload(CancellationToken cancellationToken)
        {
            var categoria = await mediator.Send
                (
                    new GetCategoriaComboReloadQuery(), cancellationToken
                );

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpGet(nameof(GetListaCategoriaPaginado))]
        public async Task<ActionResult<List<CategoriaListaDTO>>> GetListaCategoriaPaginado([FromQuery] PaginacionDTO paginacionDTO, CancellationToken cancellationToken)
        {
            var resultado = await mediator.Send
                (
                    new GetCategoriaPaginadoQuery("", paginacionDTO.Pagina, paginacionDTO.CantidadRegistrosPorPagina), cancellationToken
                );

            HttpContext.Response.Headers.Add("cantidadTotalRegistros", resultado.TotalRegistros.ToString());

            return Ok(resultado.Elementos);
        }

        [HttpGet(nameof(GetListaCategoriaFiltro))]
        public async Task<ActionResult<List<CategoriaListaDTO>>> GetListaCategoriaFiltro([FromQuery] FiltroFechaParametroDTO filtroFechaParametroDTO, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send
                (
                    new GetCategoriaQuery(filtroFechaParametroDTO.FiltroFechaParametroParametro), cancellationToken)
                );
        }

        [HttpGet(nameof(GetCategoriaPorId) + "/{id:guid}", Name = nameof(GetCategoriaPorId))]
        public async Task<ActionResult<CategoriaDTO>> GetCategoriaPorId(Guid id, CancellationToken cancellationToken)
        {
            var categoria = await mediator.Send
                (
                    new GetCategoriaByIdQuery(id), cancellationToken
                );

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }


        [HttpPost(nameof(CreatePorForm))]
        [Authorize(Policy = "CatalogWrite")]
        public async Task<IActionResult> CreatePorForm([FromForm] CategoriaCreacionDTO categoriaCreacionDTO, CancellationToken cancellationToken)
        {
            var created = await mediator.Send
                (
                    new CreateCategoriaCommand(categoriaCreacionDTO), cancellationToken
                );

            return CreatedAtAction(nameof(GetCategoriaPorId), new { id = created.Id, version = "1.0" }, created);
        }


        [HttpPut(nameof(UpdatePorForm) + "/{id:Guid}")]
        [Authorize(Policy = "CatalogWrite")]
        public async Task<IActionResult> UpdatePorForm(Guid id, [FromForm] CategoriaCreacionDTO categoriaCreacionDTO, CancellationToken cancellationToken)
        {
            var categoria = await mediator.Send
                (
                    new UpdateCategoriaCommand(id, categoriaCreacionDTO), cancellationToken
                );

            if (categoria == null)
                return NotFound();

            return Ok(categoria);

        }


        [HttpPost(nameof(ExistsCreate))]
        public async Task<bool> ExistsCreate([FromForm] CategoriaCreacionDTO categoriaCreacionDTO, CancellationToken cancellationToken)
        {
            var exists = await mediator.Send
               (
                   new ExistsCreateCategoriaCommand(categoriaCreacionDTO), cancellationToken
               );

            return exists;
        }


        [HttpPut(nameof(ExistsUpdate) + "/{id:Guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<bool> ExistsUpdate(Guid id, [FromForm] CategoriaCreacionDTO categoriaCreacionDTO, CancellationToken cancellationToken)
        {
            var exists = await mediator.Send
               (
                   new ExistsUpdateCategoriaCommand(id, categoriaCreacionDTO), cancellationToken
               );

            return exists;

        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "CatalogWrite")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            return await mediator.Send
                (
                    new DeleteCategoriaCommand(id), cancellationToken
                ) ? NoContent() : NotFound();
        }

        [HttpDelete(nameof(DeleteLogico) + "/{id:Guid}")]
        public async Task<ActionResult> DeleteLogico(Guid id, CancellationToken cancellationToken)
        {
            return await mediator.Send
                (
                    new DeleteCategoriaCommand(id), cancellationToken
                ) ? NoContent() : NotFound();
        }
    }
}
