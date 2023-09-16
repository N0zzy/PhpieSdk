namespace PhpieSdk
{

    internal interface ITest
    {
        // public string PublicA = String.Empty;
        // public static string PublicB = String.Empty;
        // protected string ProtectedA = String.Empty;
        // protected static string ProtectedB = String.Empty;

        // public string _A { set; get; }
        // public static string _B { set; get; }
        // protected string _ProtectedA { set; get; }
        // protected static string _ProtectedB { set; get; }
        // private string _PrivateA  { set; get; }
        // private static string _PrivateB { set; get; }   
        // private protected string _PrivateProtectedA { set; get; }
        // private protected static string _PrivateProtectedB { set; get; }
        
        public void M1();//interface
        public void M1P(int i, double o);//interface
        public void M2() {}//trait
        public void M2P(int i, double o) {}//trait
        private void M3() {}//trait
        private void M3P(int i, double o){}//trait
        protected void M4();//interface
        protected void M4P(int i, double o);//interface
        protected void M5() {}//trait
        protected void M5P(int i, double o) {}//trait
        private protected void M6();//interface
        private protected void M6P(int i, double o);//interface
        private protected void M7() {}//trait
        private protected void M7P(int i, double o) {}//trait
        public static void M8() {}//trait
        public static void M8P(int i, double o) {}//trait
        private static void M9() {}//trait
        private static void M9P(int i, double o) {}//trait
        protected static void M10() {}//trait
        protected static void M10P(int i, double o) {}//trait
        private protected static void M11() {}//trait
        private protected static void M11P(int i, double o) {}//trait
    }

    public class Test: ITest
    {
        // public string PublicA = String.Empty;
        // public static string PublicB = String.Empty;
        // protected string ProtectedA = String.Empty;
        // protected static string ProtectedB = String.Empty;

        // public string _A { set; get; }
        // public static string _B { set; get; }
        // protected string _ProtectedA { set; get; }
        // protected static string _ProtectedB { set; get; }
        // private string _PrivateA  { set; get; }
        // private static string _PrivateB { set; get; }   
        // private protected string _PrivateProtectedA { set; get; }
        // private protected static string _PrivateProtectedB { set; get; }

        public void T() { }
        public void T(int i) { }
        public void T(int i, int j) { }
        public void T(int i, double o) { }
        
        public static void TT() { }
        public static void TT(int i) { } 
        public static void TT(int i, int j) { }
        public static void TT(int i, double o) { }
        
        protected void TTT() { }
        protected void TTT(int i) { } 
        protected void TTT(int i, int j) { }
        protected void TTT(int i, double o) { }
        
        protected static void TTTT() { }
        protected static void TTTT(int i) { } 
        protected static void TTTT(int i, int j) { }
        protected static void TTTT(int i, double o) { }
        public virtual void M1()
        {
            throw new System.NotImplementedException();
        }

        public virtual void M1P(int i, double o)
        {
            throw new System.NotImplementedException();
        }

        public virtual void M4()
        {
            throw new System.NotImplementedException();
        }

        public virtual void M4P(int i, double o)
        {
            throw new System.NotImplementedException();
        }

        public virtual void M6()
        {
            throw new System.NotImplementedException();
        }

        public virtual void M6P(int i, double o)
        {
            throw new System.NotImplementedException();
        }
    }
}



