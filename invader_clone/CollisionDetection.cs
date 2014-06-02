using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Scott.Sprites;

namespace Scott.CollisionDetection
{
    class CollisionDetector
    {
        public static void CollideAndDestroy(List<Sprite> list1, List<Sprite> list2, SoundEffect sound)
        {
            List<Sprite> toDelete1 = new List<Sprite>();
            List<Sprite> toDelete2 = new List<Sprite>();

            foreach (Sprite spr1 in list1)
                foreach (Sprite spr2 in list2)
                    if (spr1.Hitbox().Intersects(spr2.Hitbox()))
                    {
                        sound.Play();
                        toDelete1.Add(spr1);
                        toDelete2.Add(spr2);
                    }
            foreach (Sprite del in toDelete1)
                list1.Remove(del);
            foreach (Sprite del in toDelete2)
                list2.Remove(del);
        }
    }
}
