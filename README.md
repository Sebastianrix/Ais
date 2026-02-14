This is Master-Thesis by Sebastian Rix and Neha Shamaa.
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
