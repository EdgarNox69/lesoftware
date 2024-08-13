export class ArticulosCarrito{
    id!:number;
    clienteId!:number;
    articuloId!:number;
    articulo!:{
        id:number
        descripcion:string;
        precio:number;
        imagen:string;
    }
}