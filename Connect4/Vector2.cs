namespace Connect4 {
    internal struct Vector2 {
        public Vector2(int _x, int _y) {
            x = _x;
            y = _y;
        }

        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 One = new Vector2(1, 1);
        public static readonly Vector2 Up = new Vector2(0, 1);
        public static readonly Vector2 Down = new Vector2(0, -1);
        public static readonly Vector2 Left = new Vector2(-1, 0);
        public static readonly Vector2 Right = new Vector2(1, 0);

        public int x;
        public int y;

        public static Vector2 operator +(Vector2 _a, Vector2 _b) => new Vector2(_a.x + _b.x, _a.y + _b.y);
        public static Vector2 operator -(Vector2 _a, Vector2 _b) => new Vector2(_a.x - _b.x, _a.y - _b.y);
        public static Vector2 operator *(Vector2 _a, Vector2 _b) => new Vector2(_a.x * _b.x, _a.y * _b.y);
        public static Vector2 operator *(Vector2 _a, int _b) => new Vector2(_a.x * _b, _a.y * _b);
        public static Vector2 operator /(Vector2 _a, Vector2 _b) => new Vector2((int)((float)_a.x / _b.x), (int)((float)_a.y / _b.y));
        public static Vector2 operator /(Vector2 _a, int _b) => new Vector2((int)((float)_a.x / _b), (int)((float)_a.y / _b));
        public static bool operator ==(Vector2 _a, Vector2 _b) => _a.x == _b.x && _a.y == _b.y;
        public static bool operator !=(Vector2 _a, Vector2 _b) => !(_a.x == _b.x && _a.y == _b.y);
        public static bool operator <(Vector2 _a, Vector2 _b) => (_a.x < _b.x || _a.y < _b.y);
        public static bool operator >(Vector2 _a, Vector2 _b) => (_a.x > _b.x || _a.y > _b.y);
        public static bool operator <=(Vector2 _a, Vector2 _b) => (_a.x > _b.x || _a.y > _b.y || _a.x == _b.x || _a.y == _b.y);
        public static bool operator >=(Vector2 _a, Vector2 _b) => (_a.x > _b.x || _a.y > _b.y || _a.x == _b.x || _a.y == _b.y);
        
        public override bool Equals(object o) => Equals(o);
        public override int GetHashCode() => GetHashCode();
    }
}
