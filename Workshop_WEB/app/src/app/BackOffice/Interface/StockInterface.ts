import { BookInterface } from "./BookInterface";

export interface StockInterface {
    id: number;
    createdDate: string;
    bookId: number;
    quantity: number;
    price: number;
    remark: string;
    book: BookInterface;
}
