using MultRH.Application.Users.Validators;

namespace MultRH.Application.Tests;

public class CpfValidatorTests
{
    [Theory]
    [InlineData("529.982.247-25")]
    [InlineData("52998224725")]
    public void IsValid_DeveAceitarCpfValido(string cpf)
    {
        Assert.True(CpfValidator.IsValid(cpf));
    }

    [Theory]
    [InlineData("11111111111")]
    [InlineData("12345678900")]  
    [InlineData("123")]          
    public void IsValid_DeveRejeitarCpfInvalido(string cpf)
    {
        Assert.False(CpfValidator.IsValid(cpf));
    }
}
