import axios from 'axios';

// API call to get table from database
export const fetchData = async () => {
  try {
    const response = await axios.get(`https://localhost:7147/Nba`);
    return response.data;
  } catch (error) {
    console.error('Error fetching data:', error);
    throw error;
  }
};