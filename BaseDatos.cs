using MongoDB.Driver;

public class BaseDatos {
    private string conexion = "mongodb+srv://jlcp:tommyteotianaycopito@cluster0.y0qljwv.mongodb.net/?retryWrites=true&w=majority";
    //private string conexion = "mongodb+srv://RomiTV:Retova@cluster0.i3rxnyo.mongodb.net/?retryWrites=true&w=majority";
    private string baseDatos = "Proyecto";
    public IMongoCollection<T>? ObtenerColection<T>(string coleccion){
        MongoClient client = new MongoClient(this.conexion);
        IMongoCollection<T>? collection  = client.GetDatabase(this.baseDatos).GetCollection<T>(coleccion);
        
        return collection;
    
    }
}