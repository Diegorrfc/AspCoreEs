
using Microsoft.Extensions.Configuration;
namespace AspnCore2Empty_NetClit.Services

{
    public class MensagemConfiguration : IMensagem
    {
        private IConfiguration _config;
        public MensagemConfiguration(IConfiguration config){
            _config = config;
        }
        public string Getmensagem()
        {
            return _config["Connectionstrings:DefaultConnection"];
        }
    }
}