using System;

namespace WebApi.Services{


    public class DBLogger: IloggerService{

        public void Write(string message){
            Console.WriteLine("[DBLogger] - "  + message);
        }
    }
}