using System.Collections.Generic;

public class Border {
    private Quad q;

    private List<Segment> segments;
    private List<Segment> arcs;

    public Border(Quad q, float thickness) {
        this.q = q;

        segments.Add(new Segment(q.Verts[0], q.Verts[1]));
        segments.Add(new Segment(q.Verts[1], q.Verts[2]));
        segments.Add(new Segment(q.Verts[2], q.Verts[3]));
        segments.Add(new Segment(q.Verts[3], q.Verts[0]));
        
        
    }


}
