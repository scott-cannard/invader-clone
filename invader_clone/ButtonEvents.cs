using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace invader_clone
{
    class ButtonEvents
    {
        public bool DpadLeftPress = false;
        public bool DpadLeftRelease = false;
        public bool DpadRightPress = false;
        public bool DpadRightRelease = false;
        public bool DpadUpPress = false;
        public bool DpadUpRelease = false;
        public bool DpadDownPress = false;
        public bool DpadDownRelease = false;
        public bool BackPress = false;
        public bool BackRelease = false;
        public bool StartPress = false;
        public bool StartRelease = false;
        public bool APress = false;
        public bool ARelease = false;

        public bool EscPress = false;
        public bool EnterPress = false;
        public bool SpacePress = false;
        public bool UpPress = false;
        public bool DownPress = false;

        private GamePadState previous;
        private KeyboardState previousKeys;

        public ButtonEvents(GamePadState initial, KeyboardState initialKeys)
        {
            previous = initial;
            previousKeys = initialKeys;
        }

        public void Update(GamePadState current, KeyboardState currentKeys)
        {
            //template
            //Press = (previous. == ButtonState.Released && current. == ButtonState.Pressed);
            //Release = (previous. == ButtonState.Pressed && current. == ButtonState.Released);

            DpadLeftPress = (previous.DPad.Left == ButtonState.Released && current.DPad.Left == ButtonState.Pressed);
            DpadLeftRelease = (previous.DPad.Left == ButtonState.Pressed && current.DPad.Left == ButtonState.Released);

            DpadRightPress = (previous.DPad.Right == ButtonState.Released && current.DPad.Right == ButtonState.Pressed);
            DpadRightRelease = (previous.DPad.Right == ButtonState.Pressed && current.DPad.Right == ButtonState.Released);

            DpadUpPress = (previous.DPad.Up == ButtonState.Released && current.DPad.Up == ButtonState.Pressed);
            DpadUpRelease = (previous.DPad.Up == ButtonState.Pressed && current.DPad.Up == ButtonState.Released);

            DpadDownPress = (previous.DPad.Down == ButtonState.Released && current.DPad.Down == ButtonState.Pressed);
            DpadDownRelease = (previous.DPad.Down == ButtonState.Pressed && current.DPad.Down == ButtonState.Released);

            BackPress = (previous.Buttons.Back == ButtonState.Released && current.Buttons.Back == ButtonState.Pressed);
            BackRelease = (previous.Buttons.Back == ButtonState.Pressed && current.Buttons.Back == ButtonState.Released);

            StartPress = (previous.Buttons.Start == ButtonState.Released && current.Buttons.Start == ButtonState.Pressed);
            StartRelease = (previous.Buttons.Start == ButtonState.Pressed && current.Buttons.Start == ButtonState.Released);

            APress = (previous.Buttons.A == ButtonState.Released && current.Buttons.A == ButtonState.Pressed);
            ARelease = (previous.Buttons.A == ButtonState.Pressed && current.Buttons.A == ButtonState.Released);

            EscPress = (previousKeys.IsKeyUp(Keys.Escape) && currentKeys.IsKeyDown(Keys.Escape));
            EnterPress = (previousKeys.IsKeyUp(Keys.Enter) && currentKeys.IsKeyDown(Keys.Enter));
            SpacePress = (previousKeys.IsKeyUp(Keys.Space) && currentKeys.IsKeyDown(Keys.Space));
            UpPress = (previousKeys.IsKeyUp(Keys.Up) && currentKeys.IsKeyDown(Keys.Up));
            DownPress = (previousKeys.IsKeyUp(Keys.Down) && currentKeys.IsKeyDown(Keys.Down));

            previous = current;
            previousKeys = currentKeys;
        }
    }
}
