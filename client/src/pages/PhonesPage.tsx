import { useEffect, useState } from 'react';
import Page from '../layout/Page';
import { Phone } from '../types/Phone';
import PhoneCard from '../components/cards/PhoneCard';

export default function PhonesPage() {
  const [phones, setPhones] = useState<Phone[]>([]);

  useEffect(() => {
    fetch('https://localhost:7168/api/phone/public')
      .then((response) => response.json())
      .then((phones) => setPhones(phones));
  }, []);

  return (
    <Page>
      <div className='flex gap-2 p-3'>
        {phones.map((phone, i) => (
          <PhoneCard key={i} {...phone} />
        ))}
      </div>
    </Page>
  );
}
