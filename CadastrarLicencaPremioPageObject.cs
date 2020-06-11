using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

[TestFixture]
public class CadastrarLicencaPremioPageObject
{

    public IWebDriver driver;
    public WebDriverWait wait;
    public IJavaScriptExecutor js;

    public IWebElement BuscarElementoPeloId(string id)
    {
        return driver.FindElement(By.Id(id));
    }

    public IWebElement BuscarElementoPeloXPath(string xpath)
    {
        return driver.FindElement(By.XPath(xpath));
    }

    public IWebElement BuscarElementoPeloCss(string css)
    {
        return driver.FindElement(By.CssSelector(css));
    }

    public void LimparMemoria()
    {
        string comando = "/C taskkill /F /IM chromedriver.exe /T";
        System.Diagnostics.Process.Start("CMD.exe", comando);
    }

    public CadastrarLicencaPremioPageObject(IWebDriver IWebDriver)
    {
        driver = IWebDriver;
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        js = (IJavaScriptExecutor)driver;

        wait = new WebDriverWait(driver, new TimeSpan(0, 5, 0));

        LogarNoSistema();

        NavegarAteDadosTempos();
    }

    public void Quit(IWebDriver IWebDriver)
    {
        driver.Quit();
        driver.Dispose();
        LimparMemoria();
    }

    public void PreencherForm(string dataInicio, string dataFim)
    {

        IWebElement dataInicioElement = BuscarElementoPeloId("dataInicialTempoServico");
        IWebElement dataFimElement = BuscarElementoPeloId("dataFinalTempoServico");

        dataInicioElement.Clear();
        dataFimElement.Clear();

        Thread.Sleep(500);

        dataInicioElement.SendKeys(dataInicio);
        dataFimElement.SendKeys(dataFim);
    }

    public void ConfirmarCadastro(string data)
    {
        try
        {
            BuscarElementoPeloXPath($"//*[contains(@class, 'mat-cell')][contains(text(), '{data}')]");
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public void ConfirmarNaoValidacao(string msgErroEsperada)
    {
        try
        {
            BuscarElementoPeloXPath($"//p[contains(@class, 'ng-star-inserted')][contains(text(), '{msgErroEsperada}')]");
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public bool EsperarHabilitacaoBotao()
    {
        BuscarElementoPeloXPath("//*[@id=\"cdk-accordion-child-3\"]/div/div/div[2]").Click();
        wait.Until(e => BuscarElementoPeloId("btn-addLicensaPremio").GetProperty("disabled") == "False");
        {
            IWebElement el = BuscarElementoPeloId("btn-addLicensaPremio");
            js.ExecuteScript("arguments[0].click()", el);
        }
        return true;
    }

    public void LogarNoSistema()
    {
        driver.Navigate().GoToUrl("http://siaiapconcessoesdev.tce.govrn/");
        BuscarElementoPeloId("username").Click();
        BuscarElementoPeloId("username").SendKeys("");
        BuscarElementoPeloId("password").SendKeys("");
        BuscarElementoPeloCss(".ng-input > input").Click();
        wait.Until(e => BuscarElementoPeloXPath("/html/body/ng-dropdown-panel/div/div[2]/div[2]/span")).Click();
        BuscarElementoPeloCss(".btn-login").Click();
    }

    public void NavegarAteDadosTempos()
    {

        wait.Until(e => BuscarElementoPeloXPath("//a[contains(@id, 'modulo_AtodeBenefício')][contains(text(), 'Ato de Benefício')]"));
        {
            IWebElement el = BuscarElementoPeloXPath("//a[contains(@id, 'modulo_AtodeBenefício')][contains(text(), 'Ato de Benefício')]");
            js.ExecuteScript("arguments[0].click()", el);
        }

        wait.Until(e => BuscarElementoPeloId("ConsultarAto"));
        {
            IWebElement el = BuscarElementoPeloId("ConsultarAto");
            js.ExecuteScript("arguments[0].click()", el);
        }

        wait.Until(e => BuscarElementoPeloXPath("//tr[contains(@prop, td)][td/small[contains(text(), '001509/2020')]]/td/a[contains(@prop, i)][i [contains(@title, 'Editar Ato de Benefício')] ]"));
        {
            BuscarElementoPeloXPath("//tr[contains(@prop, td)][td/small[contains(text(), '001509/2020')]]/td/a[contains(@prop, i)][i [contains(@title, 'Editar Ato de Benefício')] ]").Click();
        }

        for (int i = 0; i < 4; i++)
        {
            wait.Until(e => BuscarElementoPeloId("btn-next").GetProperty("disabled") == "False");
            {
                IWebElement el = BuscarElementoPeloId("btn-next");
                js.ExecuteScript("arguments[0].click()", el);
            }
        }
        js.ExecuteScript("window.scrollTo(0, 7.0)");
        BuscarElementoPeloCss("#mat-expansion-panel-header-3 .mat-expansion-panel-header-title").Click();
    }

}
