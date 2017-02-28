
namespace Tokiota.BookStore.Test.Integration.Api
{
    using Entities;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Web;
    using Xunit;

    public class SeriesTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public SeriesTest()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());

            _client = _server.CreateClient();
        } 
        
        [Fact]
        public async Task TestGetSeries()
        {
            // Act
            List<Serie> series = await getSeriesAsync();

            // Assert
            Assert.NotNull(series);
            Assert.True(series.Any());
        }


        [Fact]
        public async Task TestGetInitializedSeries()
        {
            // Act
            List<Serie> series = await getSeriesAsync();

            // Assert
            Assert.NotNull(series);
            Assert.Equal(5, series.Count);
        }

        [Fact]
        public async Task TestGetInitializedSeriresEnderExists()
        {
            // Act
            List<Serie> series = await getFilteredSeries("Ender's Game");

        }

        [Fact]
        public async Task TestGetSerieFirstSerie()
        {
            // Arrange
            var series = await getSeriesAsync();
            Serie targetSerie = series.First();

            // Act
            Serie serie = await getSerieAsync(targetSerie.Id);

            // Assert
            Assert.Equal(serie.Id, targetSerie.Id);
            Assert.Equal(serie.Name, targetSerie.Name);
            Assert.Equal(serie.AuthorId, targetSerie.AuthorId);
        }

        [Fact]
        public async Task TestPostBadRequest()
        {
            // Arrange
            StringContent payload = new StringContent("fake=total", Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PostAsync("/Api/Series/", payload);
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task TestCreateSerieAndCheckReturnCode()
        {
            // Arrange
            StringContent payload = createSeriePayload("my Serie");
            // Act
            var response = await _client.PostAsync("/Api/Series/", payload);
            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task TestCreateSerieAndCheckItExists()
        {
            // Arrange
            String serieName = "TestCreateSerieAndCheckItExists";
            Serie serie;
            StringContent payload = createSeriePayload(serieName, out serie);
            // Act
            var response = await _client.PostAsync("/Api/Series/", payload);
            var createdSerie = (await getFilteredSeries(serieName)).FirstOrDefault();

            // Assert
            Assert.NotNull(createdSerie);
            Assert.Equal(serie.Name, createdSerie.Name);
            Assert.Equal(serie.AuthorId, createdSerie.AuthorId);
        }

        [Fact]
        public async Task TestUpdateSerieNoId()
        {
            // Arrange
            StringContent payload = new StringContent("fake=total", Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PutAsync("/Api/Series/fff", payload);
            // Asssert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task TestUpdateSerieGuidEmpty()
        {
            // Arrange
            StringContent payload = new StringContent("fake=total", Encoding.UTF8, "application/json");
            // Act
            var response = await _client.PutAsync($"/Api/Series/{Guid.Empty.ToString()}", payload);
            // Asssert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task TestUpdateSerieBadPayload()
        {
            // Arrange
            StringContent payload = new StringContent("fake=total", Encoding.UTF8, "application/json");
            var series = await getSeriesAsync();
            var id = series.FirstOrDefault().Id;
            // Act
            var response = await _client.PutAsync($"/Api/Series/{id.ToString()}", payload);
            // Asssert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task TestUpdateSerieAndCheckStatusCode()
        {
            // Arrange
            var series = await getSeriesAsync();
            var serie = series.FirstOrDefault();
            Serie seriepayload;
            StringContent payload = createSeriePayload("New Serie", out seriepayload);

            // Act
            var response = await _client.PutAsync($"/Api/Series/{serie.Id.ToString()}", payload);
            // Asssert
            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }

        [Fact]
        public async Task TestUpdateSerieAndVerifyUpdatedSerie()
        {
            // Arrange
            var series = await getSeriesAsync();
            var serie = series.FirstOrDefault();
            Serie seriepayload;
            StringContent payload = createSeriePayload("New Serie", out seriepayload);
            // Act
            var response = await _client.PutAsync($"/Api/Series/{serie.Id.ToString()}", payload);

            // Asssert
            var updatedSerie = await getSerieAsync(serie.Id);
            Assert.Equal(seriepayload.Name, updatedSerie.Name);
        }

        [Fact]
        public async Task TestDeleteSerieNoId()
        {
            // Arrange
            // Act
            var response = await _client.DeleteAsync("/Api/Series/fff");
            // Asssert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task TestDeleteSerieGuidEmpty()
        {
            // Arrange
            // Act
            var response = await _client.DeleteAsync($"/Api/Series/{Guid.Empty.ToString()}");
            // Asssert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task TestDeleteSerie()
        {
            // Arrange
            var series = await getSeriesAsync();
            // Act
            var response = await _client.DeleteAsync($"/Api/Series/{series.FirstOrDefault().Id.ToString()}");
            // Asssert
            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }

        private static StringContent createSeriePayload(string serieName)
        {
            Serie serie;
            return createSeriePayload(serieName, out serie);
        }

        private static StringContent createSeriePayload(string serieName, out Serie serie)
        {
            serie = new Serie
            {
                Name = serieName,
            };
            var serialized = JsonConvert.SerializeObject(serie);
            return new StringContent(serialized, Encoding.UTF8, "application/json");
        }

        private async Task<Serie> getSerieAsync(Guid id)
        {
            var response = await _client.GetAsync($"/Api/Series/{id.ToString()}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var serie = JsonConvert.DeserializeObject<Serie>(responseString);
            return serie;
        }

        private async Task<List<Serie>> getFilteredSeries(string filter)
        {
            // Act
            List<Serie> series = await getSeriesAsync();

            return series.FindAll(serie => serie.Name.Equals(filter));
        }

        private async Task<List<Serie>> getSeriesAsync()
        {
            var response = await _client.GetAsync("/Api/Series");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var series = JsonConvert.DeserializeObject<List<Serie>>(responseString);
            return series;
        }

    }
}
