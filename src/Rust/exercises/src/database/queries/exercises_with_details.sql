select
    e.id,
    e.id_in_the_book,
    e.exercise_type,
    t.id as topic_id,
    t.name as topic_name,
    b.id as book_id,
    b.title as book_title,
    c.id as chapter_id,
    c.title as chapter_title,
    s.id as section_id,
    s.section_title as section_title,
    s.section_number as section_number
from
    exercises as e,
    topics as t,
    books as b,
    chapters as c,
    sections as s
where
    e.topic_id = t.id
    and e.book_id = b.id
    and e.chapter_id = c.id
    and e.section_id = s.id
;
