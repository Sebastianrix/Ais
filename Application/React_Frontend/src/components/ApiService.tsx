import axios from 'axios';
import type { Tankers } from '../types/Tankers';


const url_Base_DOTNET = "https://aismap.dk/";
//const API_URL_PYTHON = 

const URL_DOTNET = ['Stats', 'Tanker', 'TankerPositions','TankerStaging','TrackedTanker'];




export const getTankers = async (): Promise<Tankers[]> =>{
    const res = await axios.get(url_Base_DOTNET + URL_DOTNET[1]);
    return res.data;
};

export default getTankers;
