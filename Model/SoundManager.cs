using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace lLab11_U49.Model
{
   public class SoundManager
    {
        private static List<Sound> getSounds()
        {
            var sounds = new List<Sound>();
            sounds.Add(new Sound("Cow", SoundCategory.Animals));
            sounds.Add(new Sound("Cat", SoundCategory.Animals));

            sounds.Add(new Sound("Gun", SoundCategory.Cartoons));
            sounds.Add(new Sound("Spring", SoundCategory.Cartoons));

            sounds.Add(new Sound("Clock", SoundCategory.Taunts));
            sounds.Add(new Sound("Lol", SoundCategory.Taunts));

            sounds.Add(new Sound("Ship", SoundCategory.Warnings));
            sounds.Add(new Sound("Sirden", SoundCategory.Warnings));

            return sounds;

        }
        public static void GetAllSounds(ObservableCollection<Sound> sounds)
        {
            var allSound = getSounds();
            sounds.Clear();
            allSound.ForEach(p => sounds.Add(p));
        }
        public static void GetSoundByCategory(ObservableCollection<Sound> sounds ,SoundCategory soundCategory)
        {
            var allSound = getSounds();
            var filetedSounds = allSound.Where(p => p.Category == soundCategory).ToList();
        }
        public static void GetSoundByname(ObservableCollection<Sound> sounds, string name)
        {
            var allSound = getSounds();
            var filetedSounds = allSound.Where(p => p.name == name).ToList();
            sounds.Clear();
            filetedSounds.ForEach(p => sounds.Add(p));
        }
    }
}
