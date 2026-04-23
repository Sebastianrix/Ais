import axios from 'axios';
import type { Weather } from '../types/Weather';

const API_URL_DOTNET = "https://localhost:5001/weatherforecast";
//const API_URL_PYTHON = 

export const getWeather = async (): Promise<Weather[]> =>{
    const res = await axios.get(API_URL_DOTNET);
    return res.data;
};

export default getWeather;
