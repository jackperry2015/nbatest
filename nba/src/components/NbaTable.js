import React, { useEffect, useState } from 'react';
import { fetchData } from '../services/api';
import { BarChart } from '@mui/x-charts';
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';

const NbaTable = () => {
  const [data, setData] = useState([]);
  const [dataKey, setDataKey] = useState('totalWins');

  // Fetch data from an API when the component mounts
  useEffect(() => {
    const fetchDataAsync = async () => {
      const fetchedData = await fetchData();
      setData(fetchedData);
    };

    fetchDataAsync();
  }, []);

  // Data key decides while column is being displayed on the bar chart
  const handleDataKeyChange = (key) => {
    setDataKey(key);
  };

  return (
    <div className="container mx-auto my-4">
      {/* Table to display NBA data */}
      <table className="table-auto w-full mt-4 border-4 border-indigo-600">
        <thead className="border-4 border-indigo-600">
          <tr>
            <th className="px-4 py-2 border-r-2">Name</th>
            <th className="px-4 py-2 border-r-2">Stadium</th>
            <th className="px-4 py-2 border-r-2">Logo</th>
            <th className="px-4 py-2 border-r-2">Played</th>
            {/* Clickable headers for sorting */}
            <th
              className="px-4 py-2 cursor-pointer font-medium text-blue-600 dark:text-blue-500 hover:underline border-r-2"
              onClick={() => handleDataKeyChange('totalWins')}
            >
              Won
            </th>
            <th
              className="px-4 py-2 cursor-pointer font-medium text-blue-600 dark:text-blue-500 hover:underline border-r-2"
              onClick={() => handleDataKeyChange('totalLoss')}
            >
              Lost
            </th>
            <th className="px-4 py-2 border-r-2">Played Home</th>
            <th className="px-4 py-2 border-r-2">Played Away</th>
            <th className="px-4 py-2 border-r-2">Biggest Win</th>
            <th className="px-4 py-2 border-r-2">Biggest Loss</th>
            <th className="px-4 py-2 border-r-2">Last Game Stadium</th>
            <th className="px-4 py-2 border-r-2">Last Game Date</th>
            <th className="px-4 py-2 border-r-2">MVP</th>
          </tr>
        </thead>
        <tbody>
          {data.map((item) => (
            <tr key={item.name}>
              <td className="px-4 py-2 text-right border-2">{item.name}</td>
              <td className="px-4 py-2 text-righ border-2 border-b-1">{item.stadium}</td>
              <td className="px-4 py-2 text-right border-2">
                <a href={item.url} target="_blank" rel="noreferrer">
                  <img src={item.logo} alt='logo' className="w-30 h-16" />
                </a>
              </td>
              <td className="px-4 py-2 text-right border-2">{item.gamesPlayedCount}</td>
              <td className="px-4 py-2 text-right border-2">{item.totalWins}</td>
              <td className="px-4 py-2 text-right border-2">{item.totalLoss}</td>
              <td className="px-4 py-2 text-right border-2">{item.homeTeamCount}</td>
              <td className="px-4 py-2 text-right border-2">{item.awayTeamCount}</td>
              <td className="px-4 py-2 text-right border-2">{item.biggestWin}</td>
              <td className="px-4 py-2 text-right border-2">{item.biggestLoss}</td>
              <td className="px-4 py-2 text-right border-2">{item.mostRecentGameStadium}</td>
              <td className="px-4 py-2 text-right border-2">{item.mostRecentGameDate}</td>
              <td className="px-4 py-2 text-right border-2">{item.mostRecentGameMVP}</td>
            </tr>
          ))}
        </tbody>
      </table>

      {/* BarChart */}
      <div className="flex flex-col items-center pt-10">
        <h1 className="text-2xl font-bold mb-4">{dataKey}</h1>
        {/* If data from API is loaded */}
        {data.length > 0 && (
          <BarChart
            dataset={data}
            xAxis={[{ scaleType: 'band', dataKey: 'name' }]}
            series={[{ dataKey: dataKey }]}
            width={1200}
            height={600}
          />
        )}
      </div>
    </div>
  );
};

export default NbaTable;