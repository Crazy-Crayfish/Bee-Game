using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KdTree2D
{
    public class Point
    {
        public float X { get; }
        public float Y { get; }
        public Point(float x, float y) { X = x; Y = y; }

        public override string ToString() => $"({X}, {Y})";

        public Vector3 toVector ()
        {
            return new Vector3(X, Y, -20);
        }
    }

    private class Node
    {
        public Point Point;
        public Node Left;
        public Node Right;
        public int Depth;

        public Node(Point point, int depth)
        {
            Point = point;
            Depth = depth;
        }
    }

    private Node root;

    public void Insert(float x, float y)
    {
        root = Insert(root, new Point(x, y), 0);
    }

    private Node Insert(Node node, Point point, int depth)
    {
        if (node == null)
            return new Node(point, depth);

        int axis = depth % 2;

        if ((axis == 0 && point.X < node.Point.X) ||
            (axis == 1 && point.Y < node.Point.Y))
        {
            node.Left = Insert(node.Left, point, depth + 1);
        }
        else
        {
            node.Right = Insert(node.Right, point, depth + 1);
        }

        return node;
    }

    public List<Point> RangeSearch(float xMin, float xMax, float yMin, float yMax)
    {
        var result = new List<Point>();
        RangeSearch(root, xMin, xMax, yMin, yMax, result);
        return result;
    }

    private void RangeSearch(Node node, float xMin, float xMax, float yMin, float yMax, List<Point> result)
    {
        if (node == null) return;

        var p = node.Point;
        if (p.X >= xMin && p.X <= xMax && p.Y >= yMin && p.Y <= yMax)
        {
            result.Add(p);
        }

        int axis = node.Depth % 2;

        if ((axis == 0 && xMin < p.X) || (axis == 1 && yMin < p.Y))
            RangeSearch(node.Left, xMin, xMax, yMin, yMax, result);
        if ((axis == 0 && xMax >= p.X) || (axis == 1 && yMax >= p.Y))
            RangeSearch(node.Right, xMin, xMax, yMin, yMax, result);
    }

    public Point NearestNeighbor(float x, float y)
    {
        return NearestNeighbor(root, new Point(x, y), root.Point, float.MaxValue);
    }

    private Point NearestNeighbor(Node node, Point target, Point best, float bestDist)
    {
        if (node == null) return best;

        float d = DistanceSquared(node.Point, target);
        
        if (d < bestDist && !(node.Point.X == target.X && node.Point.Y == target.Y))
        {
            bestDist = d;
            best = node.Point;
        }

        int axis = node.Depth % 2;
        bool goLeftFirst = (axis == 0 && target.X < node.Point.X) || (axis == 1 && target.Y < node.Point.Y);

        Node first = goLeftFirst ? node.Left : node.Right;
        Node second = goLeftFirst ? node.Right : node.Left;

        best = NearestNeighbor(first, target, best, bestDist);
        bestDist = DistanceSquared(best, target);

        float axisDist = axis == 0 ? Math.Abs(target.X - node.Point.X) : Math.Abs(target.Y - node.Point.Y);
        if (axisDist * axisDist < bestDist)
        {
            best = NearestNeighbor(second, target, best, bestDist);
        }

        return best;
    }

    public List<Vector3> KNearestNeighbors(float x, float y, int k)
    {
        var target = new Point(x, y);
        var heap = new SortedSet<(float dist, Point point)>(Comparer<(float, Point)>.Create(
            (a, b) =>
            {
                int cmp = -a.Item1.CompareTo(b.Item1); // max-heap behavior
                return cmp == 0 ? 1 : cmp; // avoid duplicate keys in SortedSet
            }
        ));

        KNearestNeighbors(root, target, k, heap);

        // Convert to List and sort by ascending distance
        var result = new List<Vector3>();
        foreach (var item in heap)
            result.Add(item.point.toVector());

        result.Reverse(); // since we used max-heap logic

        return result;
    }

    private void KNearestNeighbors(Node node, Point target, int k, SortedSet<(float, Point)> heap)
    {
        if (node == null) return;

        float distSq = DistanceSquared(node.Point, target);

        // Ignore the target itself
        if (!(node.Point.X == target.X && node.Point.Y == target.Y))
        {
            heap.Add((distSq, node.Point));
            if (heap.Count > k)
                heap.Remove(heap.Max); // maintain only the k smallest
        }

        int axis = node.Depth % 2;
        bool goLeft = (axis == 0 && target.X < node.Point.X) || (axis == 1 && target.Y < node.Point.Y);

        Node first = goLeft ? node.Left : node.Right;
        Node second = goLeft ? node.Right : node.Left;

        KNearestNeighbors(first, target, k, heap);

        float axisDist = axis == 0
            ? Math.Abs(target.X - node.Point.X)
            : Math.Abs(target.Y - node.Point.Y);

        if (heap.Count < k || axisDist * axisDist < heap.Max.Item1)
        {
            KNearestNeighbors(second, target, k, heap);
        }
    }

    private float DistanceSquared(Point a, Point b)
    {
        float dx = a.X - b.X, dy = a.Y - b.Y;
        return dx * dx + dy * dy;
    }
}
