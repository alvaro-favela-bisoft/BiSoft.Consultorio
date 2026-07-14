using BiSoft.Consultorio.Dominio.Entidades;

namespace BiSoft.Consultorio.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            // Act
            var doctor = new Doctor("Juan Perez", "Cardiología");
            // Assert
            Assert.Equal("Juan Perez", doctor.Nombre);
            Assert.Equal("Cardiología", doctor.Especialidad);
            Assert.NotEqual(Guid.Empty, doctor.Id);
            Assert.True(doctor.Nombre.Length > 5);
            Assert.True(doctor.Nombre.Contains(' '));
            Assert.True(doctor.Nombre.Length < 50);
        }
        
        [Theory]
        [InlineData("JuanPerez", "Cardiología")]
        [InlineData("", "Pediatría")]
        [InlineData("Ana Perez", "")]
        [InlineData("", "General")]
        public void IncorrectData(string nombre, string especialidad)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => new Doctor(nombre, especialidad));
        }
    }
}
