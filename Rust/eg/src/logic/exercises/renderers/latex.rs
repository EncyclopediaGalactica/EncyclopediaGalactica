use crate::logic::exercises::repository::exercises::find_exercises_by_ids::EnrichedExerciseEntity;

pub fn render_latex(
    questions: Vec<EnrichedExerciseEntity>,
    filename_partial: String,
) -> anyhow::Result<()> {
    let mut items: String = String::from("");
    questions.iter().for_each(|question| {
        let single_question = format!(
            r#"
            \paragraph{{}}
            \begin{{tabularx}}{{1\textwidth}}{{
                    p{{\dimexpr0.5\textwidth\relax}}
                    p{{\dimexpr0.5\textwidth\relax}}
                }}
                \toprule
                Book: & {book}
                \\
                \midrule
                Chapter: & {chapter}
                \\
                \midrule
                Section:: & {section_number} - {section_title}
                \\
                \midrule
                Page: & {page}
                \\
                \midrule
                Question type: & {question_type}
                \\
                \midrule
                Question number: & {question_number}
                \\
                \bottomrule
            \end{{tabularx}}
            "#,
            book = question.book_title,
            chapter = question.chapter_title,
            section_number = question.section_number,
            section_title = question.section_title,
            page = question.section_page_exercises_start,
            question_type = question.exercise_type,
            question_number = question.id_in_book
        );
        items.push_str(&single_question);
    });
    let latex = format!(
        r#"
        \documentclass{{article}}
        \usepackage{{tabularx}}
        \usepackage{{booktabs}}
        \begin{{document}}
        {}
        \end{{document}}
        "#,
        items
    );

    match std::fs::write(&filename_partial, latex) {
        Ok(_) => Ok(()),
        Err(e) => {
            anyhow::bail!("Error while writing the file: {}", e);
        }
    }
}
