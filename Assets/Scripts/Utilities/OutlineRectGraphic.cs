using UnityEngine.Pool;

namespace UnityEngine.UI
{
    public class OutlineRectGraphic : Graphic
    {
        public float Thickness = 1f;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            var vertices = ListPool<UIVertex>.Get();
            var vert = UIVertex.simpleVert;
            vert.color = color;

            var rect = rectTransform.rect;
            float x_len = rect.size.x;
            float y_len = rect.size.y;

            var topL = new Vector2(-x_len / 2, y_len / 2);
            var topR = new Vector2(x_len / 2, y_len / 2);
            var bottomL = new Vector2(-x_len / 2, -y_len / 2);
            var bottomR = new Vector2(x_len / 2, -y_len / 2);

            // Helper method to create a quad and add it to the vertices list
            void AddQuad(Vector2 bl, Vector2 tl, Vector2 tr, Vector2 br)
            {
                vert.position = bl;
                vertices.Add(vert);

                vert.position = tl;
                vertices.Add(vert);

                vert.position = tr;
                vertices.Add(vert);

                vert.position = tr;
                vertices.Add(vert);

                vert.position = br;
                vertices.Add(vert);

                vert.position = bl;
                vertices.Add(vert);
            }

            // Left Line
            AddQuad(
                bottomL + (new Vector2(-1, -1) * Thickness),
                topL + (new Vector2(-1, 1) * Thickness),
                topL + (Vector2.up * Thickness),
                bottomL + (Vector2.down * Thickness)
            );

            // // Top Line
            AddQuad(topL, topL + Vector2.up * Thickness, topR + Vector2.up * Thickness, topR);

            // Right Line
            AddQuad(
                bottomR + (new Vector2(1, -1) * Thickness),
                topR + (new Vector2(1, 1) * Thickness),
                topR + (Vector2.up * Thickness),
                bottomR + (Vector2.down * Thickness)
            );

            // Bottom Line
            AddQuad(
                bottomL + Vector2.down * Thickness,
                bottomL,
                bottomR,
                bottomR + Vector2.down * Thickness
            );

            vh.Clear();
            vh.AddUIVertexTriangleStream(vertices);
            ListPool<UIVertex>.Release(vertices);
        }
    }
}
