﻿using EShop.Application.Features.Commands.Categories;
using EShop.Application.Features.Models;
using EShop.Application.Features.Queries.Categories;
using EShop.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(IMediator mediator) : base(mediator)
        {
        }


        [HttpGet("select-list")]
        public async Task<ActionResult<IEnumerable<SelectListItem<int>>>> GetSelectList()
        {
            var categories = await Mediator.Send(new GetCategorySelectListQuery());

            return StatusCode(StatusCodes.Status200OK, categories);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            if (id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var getByIdQuery = new GetCategoryByIdQuery
            {
                Id = id
            };

            var category = await Mediator.Send(getByIdQuery);

            return StatusCode(StatusCodes.Status200OK, category);
        }


        [HttpPost("create")]
        public async Task<ActionResult<bool>> Create(string name)
        {
            if (string.IsNullOrEmpty(name?.Trim()))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var id = await Mediator.Send(new CreateCategoryCommand(name));

            return StatusCode(StatusCodes.Status201Created, id);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            if (id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var deleteByIdCommand = new DeleteCategoryCommand
            {
                Id = id
            };

            var deleted = await Mediator.Send(deleteByIdCommand);

            return StatusCode(StatusCodes.Status204NoContent, deleted);
        }
    }
}
