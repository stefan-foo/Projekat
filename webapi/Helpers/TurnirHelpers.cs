using System.Collections.Generic;
using Models;
namespace Backend.Controllers {
    public class Helper {
        public static void SortirajUcesnike(List<Ucesnik> ucesnici) {
            int i = 0;
            bool change = true;
            while(i < ucesnici.Count){
                if (change) {
                    change = false;
                    for(int j = ucesnici.Count-1; j > i; j--){
                        if (ucesnici[j].Bodovi > ucesnici[j-1].Bodovi){
                            var pom = ucesnici[j];
                            ucesnici[j] = ucesnici[j-1];
                            ucesnici[j-1] = pom;
                            change = true;
                        }
                    }
                }
                ucesnici[i].Mesto = i+1;
                i++;
            }
        }
    }
}