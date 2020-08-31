using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lLab11_U49.Model
{
    public class Sound
    {
           public string name { get; set; }
           public SoundCategory Category { get; set; }
           public string AudioFile { get; set; }
           public string ImageFile { get; set; }
           
        public Sound(string name , SoundCategory category)
        {
                name = name;
                Category = category;
                AudioFile = String.Format("/Assets/Audio/{0}/{1}.wav", category, name);
                ImageFile = String.Format("/Assets/Images/{0}/{1}.png", category, name);

        }
    }
    public enum SoundCategory
    {
        Animals,
        Cartoons,
        Taunts,
        Warnings
    }
}
