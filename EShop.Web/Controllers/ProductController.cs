﻿using EShop.Application.Dtos.Product;
using EShop.Application.Features.Commands.ProductCommands;
using EShop.Application.Features.Queries.ProductQueries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var getByIdQuery = new GetProductByIdQuery
            {
                Id = id
            };

            var productDto = await Mediator.Send(getByIdQuery);

            return StatusCode(StatusCodes.Status200OK, productDto);
        }


        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateProductDto createProductDto)
        {
            var createCommand = createProductDto.Adapt<CreateProductCommand>();

            var productId = await Mediator.Send(createCommand);

            return CreatedAtAction(nameof(GetById), new { id = productId });
        }
    }
}
