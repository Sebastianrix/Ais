https://aismap.dk/
https://api.aismap.dk/swagger


This is Master-Thesis by Sebastian Rix and Neha Sharma.
Both Comp.Sc master graduates at Roskilde University (RUC)
Suporvisor Line Reinhard. 


This project is using Ais data
Aim is to store and promote ais data

Solution parts :

Data-API {backend} ( 
  - PostgresSQL for storage.
  - C# for Restful API.
      )

Frontend {client-side}(
  - React-APP with BEAUTIFUL map and ML features.
      )
    Setup React-TypeScript app commands bellow:
    Created with
    - npm create vite@latest
   
    Depentencies
    - npm install
    - npm install react-router-dom
    - npm install axios
    - npm install bootstrap
    - npm install @radix-ui/themes
    - npm install -D tailwindcss @tailwindcss/vite
    - npx shadcn@latest add @mapcn/map
    - npx shadcn@latest add card (this might not be nessesary, but try if the map (mapcn) is compile error.)
    - npm install @mui/material @emotion/react @emotion/styled
    - npm install lucide-react

    Run
    - npm run dev

   Docker 
   - docker compose down
   - docker compose up


