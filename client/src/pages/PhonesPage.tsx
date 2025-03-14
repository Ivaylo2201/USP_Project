import { useEffect, useState } from 'react';
import Page from '../layout/Page';
import { Phone } from '../types/Phone';
import PhoneCard from '../components/cards/PhoneCard';
import { useSearchParams } from 'react-router';

export default function PhonesPage() {
  const [phones, setPhones] = useState<Phone[]>([]);
  const [search, _] = useSearchParams();

  useEffect(() => {
    console.log(`Fetching using ${search.get('query')}`)
    fetch('https://localhost:7168/api/phone/public')
      .then((response) => response.json())
      .then((phones) => setPhones(phones));
  }, []);

  return (
    <Page>
      <div className='grid sm:grid-cols-2 md:grid-cols-3 items-center gap-5'>
        {phones.map((phone, i) => (
          <PhoneCard key={i} {...phone} />
        ))}
      </div>
    </Page>
  );
}
