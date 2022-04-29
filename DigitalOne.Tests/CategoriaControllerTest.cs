using DigitalOne.API.Controllers;
using Dio.Web.Models;
using Dio.Web.Models.Context;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DigitalOne.Tests
{

    public class CategoriaControllerTest
    {
        private readonly Mock<DbSet<Categoria>> _mockSet;
        private readonly Mock<DIOContext> _mockContext;
        private readonly Categoria _categoria;

        public CategoriaControllerTest()
        {
            _mockSet = new Mock<DbSet<Categoria>>();
            _mockContext = new Mock<DIOContext>();
            _categoria = new Categoria { Id = 1, Descricao = "TEste Categoria" };

            _mockContext.Setup(m => m.Categorias).Returns(_mockSet.Object);
            _mockContext.Setup(m => m.Categorias.FindAsync(2)).ReturnsAsync(_categoria);
        }

        [Fact]
        public async Task Get_Categoria()
        {
            var service = new CategoriaController(_mockContext.Object);

            await service.GetById(2);

            _mockSet.Verify(m => m.FindAsync(2),
            Times.Once());
        }
    }
}
