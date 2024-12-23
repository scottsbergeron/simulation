using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CitySimGame.RoadNetwork
{
    public class RoadManager : IDisposable
    {
        private List<Intersection> _intersections = new List<Intersection>();
        private List<Road> _roads = new List<Road>();
        private Intersection _pendingIntersection;
        private Texture2D _pixel;

        public RoadManager()
        {
            // We'll initialize _pixel in Draw since we need GraphicsDevice
        }

        public void HandleClick(Vector2 position)
        {
            if (_pendingIntersection == null)
            {
                // First click - create the first intersection
                _pendingIntersection = new Intersection(position);
                _intersections.Add(_pendingIntersection);
            }
            else
            {
                // Second click - create second intersection and connect with road
                var newIntersection = new Intersection(position);
                _intersections.Add(newIntersection);
                _roads.Add(new Road(_pendingIntersection, newIntersection));
                _pendingIntersection = null; // Reset for next road
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Initialize pixel texture if needed
            if (_pixel == null)
            {
                _pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _pixel.SetData(new[] { Color.White });
            }

            // Draw roads as black lines
            foreach (var road in _roads)
            {
                Vector2 direction = road.End.Position - road.Start.Position;
                float length = direction.Length();
                float angle = (float)Math.Atan2(direction.Y, direction.X);

                spriteBatch.Draw(_pixel, road.Start.Position, null, Color.Black, 
                    angle, Vector2.Zero, new Vector2(length, 3), 
                    SpriteEffects.None, 0);
            }

            // Draw intersections as red circles
            foreach (var intersection in _intersections)
            {
                spriteBatch.Draw(_pixel, new Rectangle(
                    (int)intersection.Position.X - 5,
                    (int)intersection.Position.Y - 5,
                    10, 10), Color.Red);
            }
        }

        public void Dispose()
        {
            _pixel?.Dispose();
        }
    }
} 