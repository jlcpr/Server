public static class OperacionesRequestHandler {
    public static IResult Calcular(Operaciones operaciones){
        if (operaciones.Operador.Trim() == ""){
            return Results.BadRequest("El operador es requerido");
        }
        if (operaciones.Operador != "sumar" && operaciones.Operador != "restar" &&
            operaciones.Operador != "multilicar" && operaciones.Operador != "dividir"){
                return Results.BadRequest("El operador debe tener alguno de los operadores en minúsculas: "+
                    "sumar, restar, multiplicar, dividir");
        }
        double resultado = 0;
        if (operaciones.Operador == "multiplicar" || operaciones.Operador == "dividir"){
            resultado = 1;
        }

        foreach (double numero in operaciones.Numeros){
            if (operaciones.Operador == "sumar"){
                resultado += numero;
            }
            else if(operaciones.Operador == "restar"){
                resultado -= numero;
            }
            else if(operaciones.Operador == "multiplicar"){
                resultado *= numero;
            }
            else if(operaciones.Operador == "dividir"){
                resultado /= numero;
            }
        }
        
        return Results.Ok(resultado);
    }
}