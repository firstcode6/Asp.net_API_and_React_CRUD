import { useEffect, useState } from 'react';
import './App.css';

interface Well {
    id: number;
    name: string;
    active: boolean;
    telemetryId: number;
}

function App() {
    const [wells, setWells] = useState<Well[]>();

    useEffect(() => {
        populateWellData();
    }, []);

    const contents = wells === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>Active</th>
                    <th>Telemetry id</th>
                </tr>
            </thead>
            <tbody>
                {wells.map(well =>
                    <tr key={well.id}>
                        <td>{well.id}</td>
                        <td>{well.name}</td>
                        <td>{String(well.active)}</td>
                        <td>{well.telemetryId}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Wells</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populateWellData() {
        const response = await fetch('api/Well/GetWells');
        const data = await response.json();
        setWells(data);
    }
}

export default App;