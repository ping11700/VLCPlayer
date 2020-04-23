using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VLCPlayerDemo
{
    class HttpConnection
    {

        public static void HttpConnect()
        {
            var client = new HttpClient();
            //基本的API URL
            client.BaseAddress = new Uri("http://t-beta.api.vcinema.cn/v5.0/");
            //默认使用Json序列化
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //运行client接收程序
            Run(client);
            Console.ReadLine();
        }


        //client接收处理（都是异步的处理）
        static async void Run(HttpClient client)
        {
            //post 请求插入数据
            //var result = await PostInfo(client);
            //添加结果：true

            //get 获取数据
            //var reciveData = await GetInfo(client);
            //Console.WriteLine($"查询结果：{reciveData}");

            //put 更新数据
            //result = await PutInfo(client);
            //更新结果：true

            //delete 删除数据
            //result = await DeleteInfo(client);
            //删除结果：true
        }


        //post
        static async Task<bool> PostInfo(HttpClient client)
        {
            try
            {
                //发送POST请求，Body使用Json进行序列化
                return await client.PostAsJsonAsync("movie/get_movie_url/", new MovieInfo())
                                    //返回请求是否执行成功，即 Code是否为200
                                    .ContinueWith(x => x.Result.IsSuccessStatusCode);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        //get
        static async Task<MovieInfo> GetInfo(HttpClient client)
        {
            try
            {
                //发送GET请求
                return await await client.GetAsync("movie/get_movie_url/17955/2?is_wap=1")
                                         //获取返回Body，并根据返回的Content-Type自动匹配格式化器反序列化Body内容为对象
                                         .ContinueWith(x => x.Result.Content.ReadAsAsync<MovieInfo>(
                        new List<MediaTypeFormatter>() {new JsonMediaTypeFormatter()/*这是Json的格式化器*/
                                                    ,new XmlMediaTypeFormatter()/*这是XML的格式化器*/}));
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        //put
        static async Task<bool> PutInfo(HttpClient client)
        {
            try
            {
                //发送PUT请求，Body使用Json进行序列化
                return await client.PutAsJsonAsync("movie/get_movie_url/17955/2?is_wap=1", new MovieInfo())
                                    .ContinueWith(x => x.Result.IsSuccessStatusCode);  //返回请求是否执行成功，即HTTP Code是否为2XX
            }
            catch (Exception)
            {

                throw;
            }
          
        }


        //delete
        static async Task<bool> DeleteInfo(HttpClient client)
        {
            try
            {
                //发送delete请求
                return await client.DeleteAsync("movie/get_movie_url/17955/2?is_wap=1")
                                   .ContinueWith(x => x.Result.IsSuccessStatusCode); //返回请求是否执行成功，即HTTP Code是否为2XX
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }


    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public struct MovieInfo
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
       
        [JsonProperty(PropertyName = "content")]
        public object Content { get; set; }
       
        [JsonProperty(PropertyName = "error_code")]
        public string Error_code { get; set; }
       
        [JsonProperty(PropertyName = "international_code")]
        public string International_code { get; set; }
       
        [JsonProperty(PropertyName = "error_info")]
        public string Error_info { get; set; }
       
        [JsonProperty(PropertyName = "timestamp")]
        public string Timestamp { get; set; }
       
        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }
    }
}
