import axios from 'axios';
import type { Tankers } from '../types/Tankers';


const API_URL_DOTNET = "https://aismap.dk/tanker";
//const API_URL_PYTHON = 

export const getTankers = async (): Promise<Tankers[]> =>{
    const res = await axios.get(API_URL_DOTNET);
    return res.data;
};

export default getTankers;
