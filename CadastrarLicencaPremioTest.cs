using OpenQA.Selenium;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Chrome;

[TestFixture]
public class CadastrarLicencaPremioTest
{
    CadastrarLicencaPremioPageObject _licencaPremioPageObject;
    IWebDriver _driver;

    [OneTimeSetUp]
    public void SetUp()
    {
        _driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory));
        _licencaPremioPageObject = new CadastrarLicencaPremioPageObject(_driver);
    }

    [OneTimeTearDown]
    protected void TearDown()
    {
        _licencaPremioPageObject.Quit(_driver);
    }

    [Test, Order(1)]
    public void Cadastrar_Um_Dia_Licença_Premio_1()
    {

        string dataInicio = "01/10/1998";
        string dataFim = "01/10/1998";

        _licencaPremioPageObject.PreencherForm(dataInicio, dataFim);

        _licencaPremioPageObject.EsperarHabilitacaoBotao();

        try
        {
            _licencaPremioPageObject.ConfirmarCadastro(dataInicio);
        }
        catch
        {
            Assert.That(false);
            return;
        }
        Assert.That(true);
    }

    [Test, Order(2)]
    public void Cadastrar_Um_Mes_Licença_Premio_2()
    {

        string dataInicio = "02/10/1998";
        string dataFim = "02/11/1998";

        _licencaPremioPageObject.PreencherForm(dataInicio, dataFim);

        _licencaPremioPageObject.EsperarHabilitacaoBotao();

        try
        {
            _licencaPremioPageObject.ConfirmarCadastro(dataInicio);
        }
        catch
        {
            Assert.That(false);
            return;
        }
        Assert.That(true);
    }

    [Test, Order(3)]
    public void Cadastrar_Um_Ano_Licença_Premio_3()
    {

        string dataInicio = "03/10/1996";
        string dataFim = "03/10/1997";

        _licencaPremioPageObject.PreencherForm(dataInicio, dataFim);

        _licencaPremioPageObject.EsperarHabilitacaoBotao();

        try
        {
            _licencaPremioPageObject.ConfirmarCadastro(dataInicio);
        }
        catch
        {
            Assert.That(false);
            return;
        }
        Assert.That(true);
    }

    [Test, Order(4)]
    public void Cadastrar_Licença_Premio_Outro_Periodo_Ja_Cadastrado_4()
    {

        string dataInicio = "01/10/1998";
        string dataFim = "14/12/1998";
        string msgErroEsperada = "Já existe uma licença nesse período.";

        _licencaPremioPageObject.PreencherForm(dataInicio, dataFim);

        _licencaPremioPageObject.EsperarHabilitacaoBotao();

        try
        {
            _licencaPremioPageObject.ConfirmarNaoValidacao(msgErroEsperada);
        }
        catch
        {
            Assert.Fail();
            return;
        }
        Assert.That(true);
    }

    [Test, Order(5)]
    public void Cadastrar_Licença_Premio_Com_DataInicio_Maior_Que_DataFim_5()
    {

        string dataInicio = "02/12/1998";
        string dataFim = "01/12/1998";
        string msgErroEsperada = "Essa data não pode ser menor que a inicial.";

        _licencaPremioPageObject.PreencherForm(dataInicio, dataFim);
        _licencaPremioPageObject.BuscarElementoPeloXPath("//*[@id=\"cdk-accordion-child-3\"]/div/div/div[2]").Click();

        try
        {
            _licencaPremioPageObject.ConfirmarNaoValidacao(msgErroEsperada);
        }
        catch
        {
            Assert.Fail();
            return;
        }
        Assert.That(true);
    }

    [Test, Order(6)]
    public void Cadastrar_Licença_Premio_Com_DataInicio_Menor_Que_DataIngressoCargo_6()
    {

        string dataInicio = "31/12/1994";
        string dataFim = "31/12/1994";
        string msgErroEsperada = "Essa data não pode ser menor ou igual a \"Data de Ingresso da Carreira\".";

        _licencaPremioPageObject.PreencherForm(dataInicio, dataFim);
        _licencaPremioPageObject.BuscarElementoPeloXPath("//*[@id=\"cdk-accordion-child-3\"]/div/div/div[2]").Click();

        try
        {
            _licencaPremioPageObject.ConfirmarNaoValidacao(msgErroEsperada);
        }
        catch
        {
            Assert.Fail();
            return;
        }
        Assert.That(true);
    }

    [Test, Order(7)]
    public void Cadastrar_Licença_Premio_Com_DataInicio_Maior_Que_DataVigenciaConcessao_7()
    {

        string dataInicio = "03/05/2020";
        string dataFim = "02/05/2020";
        string msgErroEsperada = "Essa data não pode ser maior que a final.";

        _licencaPremioPageObject.PreencherForm(dataInicio, dataFim);
        _licencaPremioPageObject.BuscarElementoPeloXPath("//*[@id=\"cdk-accordion-child-3\"]/div/div/div[2]").Click();

        try
        {
            _licencaPremioPageObject.ConfirmarNaoValidacao(msgErroEsperada);
        }
        catch
        {
            Assert.Fail();
            return;
        }
        Assert.That(true);
    }

    [Test, Order(8)]
    public void Cadastrar_Licença_Premio_Com_DataFim_Maior_Que_16_12_1998_9()
    {

        string dataInicio = "02/12/1998";
        string dataFim = "17/12/1998";
        string msgErroEsperada = "Essa data deve ser até 16/12/98. \"EC 20 / 1998\".";

        _licencaPremioPageObject.PreencherForm(dataInicio, dataFim);
        _licencaPremioPageObject.BuscarElementoPeloXPath("//*[@id=\"cdk-accordion-child-3\"]/div/div/div[2]").Click();

        try
        {
            _licencaPremioPageObject.ConfirmarNaoValidacao(msgErroEsperada);
        }
        catch
        {
            Assert.Fail();
            return;
        }
        Assert.That(true);
    }

}
