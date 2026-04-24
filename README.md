This is Master-Thesis by Sebastian Rix and Neha Sharma.
Both Comp.Sc master graduates at Roskilde University (RUC)
Suporvisor Line Reinhard. 


This project is using Ais data and Machine learning.
Aim is to visualize and detect Russian Shadowfleed vessels.

Solution parts :

Data-API {backend} (
  - Python-flask-server for ETL-pipline. 
  - PostgresSQL for storage.
  - C# or JAVA for Restful API.
      )

Prediction-model-API {backend} (
  - Python-flask-server (predicting with the pretrained models)
      )
    
Prediction-model-Pretrained {Needs GPU}(
  - Notebooks with model definition and hyper-paramaters.
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

    Run
    - npm run dev

   Docker 
   - docker compose down
   - docker compose up


