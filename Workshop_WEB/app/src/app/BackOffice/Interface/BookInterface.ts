import { PublisherInterface } from "./PublisherInterface";

interface BookInterface {
    id: number;
    name: string;
    isbn: string;
    price: number;
    publisherId: number;
    publisher: PublisherInterface;
}

export type { BookInterface };