import Page from '../layout/Page';
import PhoneCard from '../components/cards/PhoneCard';
import { useSearchParams } from 'react-router';
import usePhones from '../hooks/usePhones';
import { Suspense } from 'react';
import { Loader } from '@mantine/core';

export default function PhonesPage() {
  const [search] = useSearchParams();
  const { data: phones } = usePhones(Object.fromEntries(search.entries()));

  return (
    <Page>
      <Suspense fallback={<Loader color={'var(--color-blue-400)'} />}>
        <div className='flex flex-wrap justify-center items-center gap-5'>
          {phones.map((phone, i) => (
            <PhoneCard key={i} {...phone} />
          ))}
        </div>
      </Suspense>
    </Page>
  );
}
