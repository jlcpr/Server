public static class CalificacionesRequestHandlers {
    public static IResult MostrarCalificaciones(long numControl, 
        int parcial, bool soloAsignaturas, bool soloAcreditadas){
        if(numControl == 22328051050163){
        List<CalificacionMateria> list =new List<CalificacionMateria>();

        CalificacionMateria m1 = new CalificacionMateria();
        m1.Calificacion = 10;
        m1.Materia = "Programacion Orientada a Objetos";
        m1.Parcial = 1;
        m1.NumControl = 22328051050163;

        CalificacionMateria m2 = new CalificacionMateria();
        m2.Calificacion = 9;
        m2.Materia = "Programacion Orientada a Eventos";
        m2.Parcial = 1;
        m2.NumControl = 22328051050163;

        CalificacionMateria m3 = new CalificacionMateria();
        m3.Calificacion = 7.2;  
        m3.Materia = "Geometria";
        m3.Parcial = 1;
        m3.NumControl = 22328051050163;
        
        CalificacionMateria m4 = new CalificacionMateria();
        m4.Calificacion = 10;  
        m4.Materia = "Ingles";
        m4.Parcial = 1;
        m4.NumControl = 22328051050163;
        
        CalificacionMateria m5 = new CalificacionMateria();
        m5.Calificacion = 9.5;  
        m5.Materia = "Biologia";
        m5.Parcial = 1;
        m5.NumControl = 22328051050163;

        CalificacionMateria m6 = new CalificacionMateria();
        m6.Calificacion = 9.8;  
        m6.Materia = "Etica";
        m6.Parcial = 1;
        m6.NumControl = 22328051050163;

        CalificacionMateria m7 = new CalificacionMateria();
        m7.Calificacion = 10;  
        m7.Materia = "Tutoria";
        m7.Parcial = 1;
        m7.NumControl = 22328051050163;

        list.Add(m1);
        list.Add(m2);
        list.Add(m3);
        list.Add(m4);
        list.Add(m5);
        list.Add(m6);
        list.Add(m7);

        return Results.Ok(list);
    }
    else{
        return Results.NotFound($"No existe un alumno con número de control {numControl}");
    }
}
}