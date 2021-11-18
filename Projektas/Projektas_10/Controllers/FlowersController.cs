using Microsoft.AspNetCore.Mvc;
using Projektas_10.Models;
using Projektas_10.Services;
using System.Collections.Generic;

namespace Projektas_10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowersController : ControllerBase
    {
       [HttpPost] 
       public IActionResult Create(Flower flower)
        {
            if(flower.FlowerId == "")
            {
                return ValidationProblem("Not specified flower id!");
            }

            if (flower.FlowerName == "")
            {
                return ValidationProblem("Not specified flower name!");
            }

            if (flower.FlowerKind == "")
            {
                return ValidationProblem("Not specified flower kind!");
            }

            if (flower.FlowerStartingPrice == 0)
            {
                return ValidationProblem("Not specified flower starting price!");
            }

            if (flower.FlowerDiscount < 0)
            {
                return ValidationProblem("Not specified flower discount price!");
            }

            if (flower.FlowerDescription == "")
            {
                return ValidationProblem("Not specified flower description!");
            }

            var service = new FlowerService();
            service.CreateFlower(flower);

            return Ok();
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            var service = new FlowerService();
            var flowers = service.GetFlowers();

            return new OkObjectResult(flowers);
        }

        [HttpGet]
        public IActionResult Get(string flowerId)
        {
            var service = new FlowerService();
            var flower = service.GetFlower(flowerId);

            return new OkObjectResult(flower);
        }

        [HttpGet("filter")]
        public IActionResult Filter(string text)
        {
            var service = new FlowerService();
            var flowers = service.GetFlowers();

            var filteredFlowers = new List<Flower>();

            foreach (var flower in flowers)
            {
                if (flower.GetInformation().Contains(text))
                {
                    filteredFlowers.Add(flower);
                }
            }

            return new OkObjectResult(filteredFlowers);
        }
    }
}
